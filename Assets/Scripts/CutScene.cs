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
    public GameObject knife; //S� den kan slukkes i cutscene
    public LineRenderer lineRenderer;

    //Rating ting
    public GameObject ratingMessage;
    public GameObject Line; //for at f� adgang til drawing script og dermed accuracy dist
    private GameObject TheRating; // Et GameObject der starter tomt men senere s�ttes den til at v�re ratingen. Derefter kan ratingen kaldes i udenfor Rating() og slukkes i CutSceneInScene() 
    public Drawing Drawing;


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Rating()
    {
        Debug.Log("Vi er inde i rating funktionen.");
        //Drawing drawing = Line.GetComponent<Drawing>(); //f� adgang til drawing script
        float accuracyDist = Drawing.GetAccuracyDist(); //F� adgang til accuracyDist

        ratingMessage.SetActive(true);
        //Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingMessage.transform.childCount; i++)
        {
            Transform t = ratingMessage.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }

        

        GameObject rating = null;

        if (accuracyDist > 0.4)
        {
            Debug.Log("Vi er inde i S ifs�tningens.");
            rating = ratingMessage.transform.Find("S").gameObject;
        }

        if (accuracyDist > 0.4 && accuracyDist < 0.5)
        {
            rating = ratingMessage.transform.Find("A").gameObject;
        }

        if (accuracyDist > 0.5 && accuracyDist < 0.55)
        {
            rating = ratingMessage.transform.Find("B").gameObject;
        }

        if (accuracyDist > 0.55 && accuracyDist < 0.65)
        {
            rating = ratingMessage.transform.Find("C").gameObject;
        }

        if (accuracyDist > 0.65 && accuracyDist < 0.75)
        {
            rating = ratingMessage.transform.Find("D").gameObject;
        }

        if (accuracyDist > 0.75)
        {
            rating = ratingMessage.transform.Find("F").gameObject;
        }

        if (rating == null)
        {
            // Print a log message if the GameObject doesn't exist
            Debug.LogError("The rating does not exist.");
            return;

        }

        Debug.Log("Rating er valgt");
        rating.SetActive(true);
        Debug.Log("Rating er t�ndt");

        //G�r s� vi kan kalde p� ratingen udenfor funktionen for at slukke den i CutSceneInScene funktionen
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

        //Slukker for Drawing script
        //Drawing drawing = Line.GetComponent<Drawing>();
        Drawing.enabled = false;

        //Placer spilleren i det rigtige sted 
        Player.position = CutScenePos.position;

        //kniven skal slukkes
        knife.SetActive(false);

        //g�r linjerne gennemsigitg. Hvis linjerne bare slettes s� er der ikke en position der kan bruges til at bestemme den rating man skal f� senere i rating()
        Drawing.SlukRendererForAlleLinjer();

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd
        CutSceneTestText.SetActive(true); //Det er bare en placeholder tester

        yield return new WaitForSeconds(CutSceneTime); //kode til lidt pause s� man kan se Cutscene f�r ratingen popper up
        Rating();

        //Linjerne skal slettes efter de er blevet brugt til at bestemme en rating
        Drawing.DeleteAllLines();
        Drawing.T�ndRendererForAlleLinjer(); //man kan se linjer til n�ste gang
        yield return new WaitForSeconds(RatingReadTime); //Lidt tid til at l�se sin rating

        //G�r klar til fortsat spil
        MainCam.enabled = true;
        CutSceneCam.SetActive(false);
        CutSceneTestText.SetActive(false); //Det er bare en placeholder tester
        TheRating.SetActive(false); //Sletter ratingen p� sk�rmen s� man kan spille videre
    
        //Reset spillerens position i k�kkenet?

        //T�nder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true;
        //ratingMessage.SetActive(false);



        //FOR AT VISE ALLE RATINGS TIL SIDST
        //RatingManager.DisplayRatings(); //Det er en test for at se om ratingen bliver gemt i listen som denne funktion printer
    }

    

}
