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
        yield return new WaitForSeconds(messageWaitTime); //kode til lidt pause så man kan se beskeden


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
        ratingMessage.SetActive(true);
        //HUSK JO MINDRE ACCURACYDIST JO BEDRE

        // Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingMessage.transform.childCount; i += 1)
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

            if (rating == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'A' rating does not exist.");

            }
        }

        if (accuracyDist > 0.5 && accuracyDist < 0.6)
        {
            rating = ratingMessage.transform.Find("B").gameObject;

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'B' rating does not exist.");

            }
        }
        if (accuracyDist > 0.6 && accuracyDist < 0.7)
        {
            rating = ratingMessage.transform.Find("C").gameObject;

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'C' rating does not exist.");

            }
        }
        if (accuracyDist > 0.7 && accuracyDist < 0.8)
        {
            rating = ratingMessage.transform.Find("D").gameObject;

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'D' rating does not exist.");

            }
        }
        if (accuracyDist > 0.8)
        {
            rating = ratingMessage.transform.Find("F").gameObject;

            if (ratingMessage == null)
            {
                // Print a log message if the GameObject doesn't exist
                Debug.LogError("The 'F' rating does not exist.");
            }
        }
        if (rating == null)
        {
            // Print a log message if the GameObject doesn't exist
            Debug.LogError("The rating does not exist.");
            return;

        }
        rating.SetActive(true);
    }
    void CutSceneInScene()
    {
        //MainCam.enabled(false);
        //CutSceneCam.enabled(true);
        //Afspil animationer
        //Skift scene = SceneManager.LoadScene(NewSceneNumber);
    }
}


