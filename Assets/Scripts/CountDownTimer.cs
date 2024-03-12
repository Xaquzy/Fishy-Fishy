using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements.Experimental;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    public float remainingTime = 5f;
    public GameObject ratingMessage;
    private int accuracy;//slettes n�r rating funktionen er f�rdig. Kig l�ngere nede i koden.

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
        yield return new WaitForSeconds(remainingTime + 1);
        Debug.Log("Go to cutscene");
        //SceneManager.LoadScene(NewSceneNumber);
        ratingMessage.SetActive(true); //viser dig den rating du fik
        //kode til lidt pause s� man kan se beskeden
        ratingMessage.SetActive(false);// fjerner den igen f�r du g�r videre
    }

    void Rating()
    {
        //Basmala t�nker kode hvor vi kalder p� int'en fra Drawing scriptet, der beregner forskel
        //lad os sige vi kalder forskellen 'accuracy', har bare lavet den til en privat int der ikk findes lige nu s� den ikke bliver sur
        //tallene er ogs� random da vi endnu ikke ved hvilke tal er realistiske
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
}


