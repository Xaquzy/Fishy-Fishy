using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements.Experimental;
using Cinemachine;

public class CountDownTimer : MonoBehaviour
{
    public float remainingTime = 5f;
    [SerializeField] TextMeshProUGUI countdownText;
    public GameObject ratingMessage;
    public GameObject Line; //for at få adgang til drawing script og dermed accuracy dist

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

    //[SerializeField] private int NewSceneNumber = 1;

    void Start()
    {
        ratingMessage.SetActive(false);
    }

    void Update()
    {


        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime; //trækker den tid der er gået fra den tid der er tilbage
        }

        if (remainingTime <= 4 && remainingTime > 1)
        {
            countdownText.color = Color.yellow; //Gør teksten gul når der er under 4 (3) sekunder tilbage
        }

        if (remainingTime <= 1)
        {
            remainingTime = 0; //Sætter tiden til 0 så timeren ikke kan blive negativ
            countdownText.color = Color.red; //Gør teksten rød
            Debug.Log("Go to cutscene");
            StartCoroutine(CutSceneInScene()); //kalder på sceneskift funktionen


        }

        int minutes = Mathf.FloorToInt(remainingTime / 60); //Omregner mængden af sekunder til minuter
        int seconds = Mathf.FloorToInt(remainingTime % 60); //Omregner mængden af tid til sekunder der er til rest efter minutterne
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //string format, gør så det ser pænt ud

    }

 
    void Rating()
    {
        Drawing drawing = Line.GetComponent<Drawing>(); //få adgang til drawing script
        float accuracyDist = drawing.GetAccuracyDist(); //Få adgang til accuracyDist
        ratingMessage.SetActive(true);


        // Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingMessage.transform.childCount; i++)
        {
            Transform t = ratingMessage.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }
        GameObject rating = null;

        if (accuracyDist > 0.4)
        {
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
        rating.SetActive(true);

        RatingManager.AddRating(rating.name); //Konverter rating navn til string

    }
    IEnumerator CutSceneInScene()
    {
        //Slukker for movement script
        Movement movement = Player.GetComponent<Movement>(); 
        movement.enabled = false;

        //Slukker for Drawing script
        Drawing drawing = Line.GetComponent<Drawing>();
        drawing.enabled = false;

        //Placer spilleren i det rigtige sted 
        Player.position = CutScenePos.position;

        //kniven skal slukkes
        knife.SetActive(false);

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;
        CutSceneTestText.SetActive(true); //Det er bare en placeholder tester

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd

        yield return new WaitForSeconds(CutSceneTime); //kode til lidt pause så man kan se Cutscene før ratingen popper up
        Rating();
        yield return new WaitForSeconds(RatingReadTime); //Lidt tid til at læse sin rating

        //Gør klar til fortsat spil
        MainCam.enabled = true;
        CutSceneCam.SetActive(false);
        //Skift fiske objektFunktion
        //Reset spillerens position i køkkenet?

        //Tænder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true;
        ratingMessage.SetActive(false);


        //Sluk CountDownTimer script.. altså det script vi er i. Dette er så remaingTime kan genstartes til næste cut i fisken
        enabled = false;

        //FOR AT VISE ALLE RATINGS TIL SIDST
        //RatingManager.DisplayRatings(); //Det er en test for at se om ratingen bliver gemt i listen som denne funktion printer
    }
}


