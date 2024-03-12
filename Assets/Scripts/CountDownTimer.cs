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
    public GameObject Line; //for at f� adgang til drawing script og dermed accuracy dist
    //Cutscene ting
    //[SerializeField] private int NewSceneNumber = 1;

    void Update()
    {


        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime; //tr�kker den tid der er g�et fra den tid der er tilbage
        }

        if (remainingTime <= 4 && remainingTime > 1)
        {
            countdownText.color = Color.yellow; //G�r teksten gul n�r der er under 4 (3) sekunder tilbage
        }

        if (remainingTime <= 1)
        {
            remainingTime = 0; //S�tter tiden til 0 s� timeren ikke kan blive negativ
            countdownText.color = Color.red; //G�r teksten r�d



            StartCoroutine(FinishCut()); //kalder p� sceneskift funktionen

        }

        int minutes = Mathf.FloorToInt(remainingTime / 60); //Omregner m�ngden af sekunder til minuter
        int seconds = Mathf.FloorToInt(remainingTime % 60); //Omregner m�ngden af tid til sekunder der er til rest efter minutterne
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //string format, g�r s� det ser p�nt ud

    }

    //Scenechange efter x sekunders cutscene
    IEnumerator FinishCut()
    {
        Rating(); //kalder p� rating funktionen n�r tiden er over, aka vurdering af ens cut
        ratingMessage.SetActive(true); //viser dig den rating du fik
        yield return new WaitForSeconds(messageWaitTime); //kode til lidt pause s� man kan se beskeden
        ratingMessage.SetActive(false);// fjerner den igen f�r du g�r videre

        Debug.Log("Go to cutscene");
        //To muligheder til cutscene
        //Skift scene = SceneManager.LoadScene(NewSceneNumber);

        //Skift kamera og afspil animation = at kalded p� nedenst�ende funktion (funktionen er tom lige nu)
        CutSceneInScene();
    }
    void Rating()
    {
        Drawing drawing = Line.GetComponent<Drawing>(); //f� adgang til drawing script
        float accuracy = drawing.GetAccuracyDist(); //F� adgang til accuracyDist

        if (accuracy > 10)
        {
            ratingMessage = GameObject.Find("S");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'S' rating does not exist.");

            }
        }
        if (accuracy > 7 && accuracy < 10)
        {
            ratingMessage = GameObject.Find("A");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'A' rating does not exist.");

            }
        }

        if (accuracy > 4 && accuracy < 7)
        {
            ratingMessage = GameObject.Find("B");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'B' rating does not exist.");

            }
        }
        if (accuracy > 2 && accuracy < 4)
        {
            ratingMessage = GameObject.Find("C");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'C' rating does not exist.");

            }
        }
        if (accuracy > 0 && accuracy < 2)
        {
            ratingMessage = GameObject.Find("D");

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'D' rating does not exist.");

            }
        }
        if (accuracy > -2 && accuracy < 0)
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


