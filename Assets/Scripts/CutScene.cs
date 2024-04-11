using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

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
    public GameObject hose; //Så den kan slukkes i cutscene
    public GameObject hand; //Så den kan slukkes i cutscene
    public LineRenderer lineRenderer;
    public GameObject CountdownTimerText; //Så den kan slukkes i cutscene
    public CookZone CookZone;


    //Rating ting
    public GameObject ratingMessage;
    public GameObject Line; 
    private GameObject TheRating; // Et GameObject der starter tomt men senere sættes den til at være ratingen. Derefter kan ratingen kaldes i udenfor Rating() og slukkes i CutSceneInScene() 
    public Drawing Drawing;
    public DropObjZone DropObjZone;
    public RatingManager RatingManager;


    // Update is called once per frame
    void Update()
    {

    }
    
    public void Rating()
    {
        Debug.Log("Vi er inde i rating funktionen.");
        float accuracyDist = Drawing.GetAccuracyDist(); //Få adgang til accuracyDist
        ratingMessage.SetActive(true);
        
        //Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingMessage.transform.childCount; i++)
        {
            Transform t = ratingMessage.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }
        GameObject rating = null;

        //Ratings for drab/drop
        if (float.IsNaN(accuracyDist)) //Hvis accucacy ikke er et tal (NaN). Vi bestemte dette ved at debug.log for at finde ud af hvad vi skal hae i if-sætningen ud fra accacydist. vi startede med if accuracyDist = null
        {

            if (DropObjZone.ZoneScore >= 5)
            {
                rating = ratingMessage.transform.Find("F").gameObject;
            }
            if (DropObjZone.ZoneScore == 4)
            {
                rating = ratingMessage.transform.Find("D").gameObject;
            }
            if (DropObjZone.ZoneScore == 3)
            {
                rating = ratingMessage.transform.Find("C").gameObject;
            }
            if (DropObjZone.ZoneScore == 2)
            {
                rating = ratingMessage.transform.Find("B").gameObject;
            }
            if (DropObjZone.ZoneScore == 1)
            {
                rating = ratingMessage.transform.Find("A").gameObject;
            }
            if (DropObjZone.ZoneScore == 0)
            {
                rating = ratingMessage.transform.Find("S").gameObject;
            }
        }
        
        //Ratings for tegne
        else
        {
            Debug.Log("Accuracy Dist: " + accuracyDist);
            if (accuracyDist > 0.00001 && accuracyDist < 0.52)
            {
                Debug.Log("Vi er inde i S if sætningens.");
                rating = ratingMessage.transform.Find("S").gameObject;
            }
            if (accuracyDist > 0.52 && accuracyDist < 0.58)
            {
                rating = ratingMessage.transform.Find("A").gameObject;
            }
            if (accuracyDist > 0.58 && accuracyDist < 0.63)
            {
                rating = ratingMessage.transform.Find("B").gameObject;
            }
            if (accuracyDist > 0.63 && accuracyDist < 0.70)
            {
                rating = ratingMessage.transform.Find("C").gameObject;
            }
            if (accuracyDist > 0.70 && accuracyDist < 0.9)
            {
                rating = ratingMessage.transform.Find("D").gameObject;
            }
            if (accuracyDist > 0.9)
            {
                rating = ratingMessage.transform.Find("F").gameObject;
            }
        }
        

        Debug.Log("Rating er valgt");

        rating.SetActive(true);

        Debug.Log("Rating er tændt");

        //Gør så vi kan kalde på ratingen udenfor funktionen for at slukke den i CutSceneInScene funktionen
        TheRating = rating;
        RatingManager.AddRating(rating);
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

        //objekterne skal slukkes
        knife.SetActive(false);
        hose.SetActive(false);
        hand.SetActive(false);

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
        RatingManager.DisplayRatings(); //Det er en test for at se om ratingen bliver gemt i listen som denne funktion printer
    }
}
