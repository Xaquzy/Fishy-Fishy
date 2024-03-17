using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements.Experimental;

public class CountDownTimer : MonoBehaviour
{
    public float remainingTime = 5f;
    public float messageWaitTime = 5f;
    [SerializeField] TextMeshProUGUI countdownText;
    public GameObject ratingMessage;
    public GameObject Line; //for at få adgang til drawing script og dermed accuracy dist
    //Cutscene ting
    //[SerializeField] private int NewSceneNumber = 1;

    private void Start()
    {
        
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



            StartCoroutine(FinishCut()); //kalder på sceneskift funktionen

        }

        int minutes = Mathf.FloorToInt(remainingTime / 60); //Omregner mængden af sekunder til minuter
        int seconds = Mathf.FloorToInt(remainingTime % 60); //Omregner mængden af tid til sekunder der er til rest efter minutterne
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //string format, gør så det ser pænt ud

    }

    //Scenechange efter x sekunders cutscene
    IEnumerator FinishCut()
    {
        Rating(); //kalder på rating funktionen når tiden er over, aka vurdering af ens cut
        ratingMessage.SetActive(true); //viser dig den rating du fik
        yield return new WaitForSeconds(messageWaitTime); //kode til lidt pause så man kan se beskeden
        ratingMessage.SetActive(false);// fjerner den igen før du går videre

        Debug.Log("Go to cutscene");
        //To muligheder til cutscene
        //Skift scene = SceneManager.LoadScene(NewSceneNumber);

        //Skift kamera og afspil animation = at kalded på nedenstående funktion (funktionen er tom lige nu)
        CutSceneInScene();
    }
    void Rating()
    {
        Drawing drawing = Line.GetComponent<Drawing>(); //få adgang til drawing script
        float accuracyDist = drawing.GetAccuracyDist(); //Få adgang til accuracyDist
        //HUSK JO MINDRE ACCURACYDIST JO BEDRE

        if (accuracyDist > 10)
        {
            ratingMessage = GameObject.Find("S");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'S' rating does not exist.");

            }
        }
        if (accuracyDist > 7 && accuracyDist < 10)
        {
            ratingMessage = GameObject.Find("A");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'A' rating does not exist.");

            }
        }

        if (accuracyDist > 4 && accuracyDist < 7)
        {
            ratingMessage = GameObject.Find("B");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'B' rating does not exist.");

            }
        }
        if (accuracyDist > 2 && accuracyDist < 4)
        {
            ratingMessage = GameObject.Find("C");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'C' rating does not exist.");

            }
        }
        if (accuracyDist > 0 && accuracyDist < 2)
        {
            ratingMessage = GameObject.Find("D");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'D' rating does not exist.");

            }
        }
        if (accuracyDist > -2 && accuracyDist < 0)
        {
            ratingMessage = GameObject.Find("F");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'F' rating does not exist.");
            }
        }
    }
    void CutSceneInScene()
    {
        //MainCam.enabled(false);
        //CutSceneCam.enabled(true);
        //Afspil animationer
        //Skift scene = SceneManager.LoadScene(NewSceneNumber);
    }
}


