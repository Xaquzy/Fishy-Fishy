using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutScene : MonoBehaviour
{
    //Cutscene ting
    public Transform Player;
    public Transform CutScenePos;
    public CinemachineFreeLook MainCam;
    public GameObject CutSceneCam;
    public GameObject CookCam;
    public GameObject CutSceneTestText;
    public float CutSceneTime = 5f;
    public float RatingReadTime = 5f;
    public GameObject knife; //Så den kan slukkes i cutscene
    public LineRenderer lineRenderer;
    public GameObject CountdownTimerText; //Så den kan slukkes i cutscene
    public CookZone CookZone;

    //Rating ting
    public GameObject ratingMessage;
    public GameObject Line; 
    private GameObject TheRating; // Et GameObject der starter tomt men senere sættes den til at være ratingen. Derefter kan ratingen kaldes i udenfor Rating() og slukkes i CutSceneInScene() 
    public Drawing Drawing;
    public DropObjZone DropObjZone;


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Rating()
    {
        Debug.Log("Vi er inde i rating funktionen.");
        float accuracyDist = Drawing.GetAccuracyDist(); //Få adgang til accuracyDist
        Debug.Log("1:" + accuracyDist);
        ratingMessage.SetActive(true);
        
        //Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingMessage.transform.childCount; i++)
        {
            Transform t = ratingMessage.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }
        Debug.Log("2:" + accuracyDist);
        GameObject rating = null;

        // Default rating if DropObjZone is not available
        rating = ratingMessage.transform.Find("S").gameObject;
        Debug.Log("3:" + accuracyDist);
        // Ratings for drawing
        if (accuracyDist != 0)
        {
            Debug.Log("4:" + accuracyDist);
            if (accuracyDist > 0.00001 && accuracyDist < 0.4)
            {
                Debug.Log("Vi er inde i S ifsætningens.");
                rating = ratingMessage.transform.Find("S").gameObject;
            }
            else if (accuracyDist > 0.4 && accuracyDist < 0.5)
            {
                rating = ratingMessage.transform.Find("A").gameObject;
            }
            else if (accuracyDist > 0.5 && accuracyDist < 0.55)
            {
                rating = ratingMessage.transform.Find("B").gameObject;
            }
            else if (accuracyDist > 0.55 && accuracyDist < 0.65)
            {
                rating = ratingMessage.transform.Find("C").gameObject;
            }
            else if (accuracyDist > 0.65 && accuracyDist < 0.75)
            {
                rating = ratingMessage.transform.Find("D").gameObject;
            }
            else if (accuracyDist > 0.75)
            {
                Debug.Log("5:" + accuracyDist);
                rating = ratingMessage.transform.Find("F").gameObject;
            }
            Debug.Log("6:" + accuracyDist);
        }
        Debug.Log("7:" + accuracyDist);
        // Ratings for DropObjZone
        if (DropObjZone != null)
        {
            Debug.Log("Vi er i drop tingen lalala");
            //Debug.Log("8:" + accuracyDist);
            //if (DropObjZone.ZoneScore >= 5)
            //{
            //    rating = ratingMessage.transform.Find("F").gameObject;
            //}
            //else if (DropObjZone.ZoneScore == 4)
            //{
            //    rating = ratingMessage.transform.Find("D").gameObject;
            //}
            //else if (DropObjZone.ZoneScore == 3)
            //{
            //    rating = ratingMessage.transform.Find("C").gameObject;
            //}
            //else if (DropObjZone.ZoneScore == 2)
            //{
            //    rating = ratingMessage.transform.Find("B").gameObject;
            //}
            //else if (DropObjZone.ZoneScore == 1)
            //{
            //    rating = ratingMessage.transform.Find("A").gameObject;
            //}
            //else if (DropObjZone.ZoneScore == 0)
            //{
            //    rating = ratingMessage.transform.Find("S").gameObject;
            //}
        }
        Debug.Log("Rating er valgt");

        rating.SetActive(true);

        Debug.Log("Rating er tændt");

        //Gør så vi kan kalde på ratingen udenfor funktionen for at slukke den i CutSceneInScene funktionen
        TheRating = rating;

        //RatingManager.AddRating(rating.name); //Konverter rating navn til string
    }


    public void StartCutScene()
    {
        StartCoroutine(CutSceneInScene());
    }
    public IEnumerator CutSceneInScene()
    {
        //Slukker for movement script
        Movement movement = Player.GetComponent<Movement>();
        movement.enabled = false;

        //Slukker for alle egenskaber
        CookZone.SlukEgenskaber();

        //Placer spilleren i det rigtige sted 
        Player.position = CutScenePos.position;

        //kniven skal slukkes
        knife.SetActive(false);

        //gør linjerne gennemsigitg. Hvis linjerne bare slettes så er der ikke en position der kan bruges til at bestemme den rating man skal få senere i rating()
        Drawing.DisableAllLineRenderer();

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd
        CutSceneTestText.SetActive(true); //Det er bare en placeholder tester

        yield return new WaitForSeconds(CutSceneTime); //kode til lidt pause så man kan se Cutscene før ratingen popper up
        Rating();

        //Linjerne skal slettes efter de er blevet brugt til at bestemme en rating
        Drawing.DeleteAllLines();
        Drawing.EnableAllLineRenderer(); //man kan se linjer til næste gang
        yield return new WaitForSeconds(RatingReadTime); //Lidt tid til at læse sin rating

        //Gør klar til fortsat spil
        CountdownTimerText.SetActive(false); //Timer texten slukkes
        MainCam.enabled = true;
        CutSceneCam.SetActive(false);
        CutSceneTestText.SetActive(false); //Det er bare en placeholder tester
        TheRating.SetActive(false); //Sletter ratingen på skærmen så man kan spille videre
        knife.SetActive(true); //Kniven skal findes igen

        //Reset spillerens position i køkkenet?

        //Tænder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true; //.SetActive(false);



        //FOR AT VISE ALLE RATINGS TIL SIDST
        //RatingManager.DisplayRatings(); //Det er en test for at se om ratingen bliver gemt i listen som denne funktion printer
    }
}
