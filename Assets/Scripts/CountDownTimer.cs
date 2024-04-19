using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements.Experimental;
using Cinemachine;

public class CountDownTimer : MonoBehaviour
{
    public float CountdownTime = 5f;
    public float remainingTime;
    [SerializeField] TextMeshProUGUI countdownText;
    [HideInInspector] public bool timer_running = false;
    public int TotalCutScenes = 12;
    private int CutScenesPlayed = 0;
    
    //Henter cutscene scriptet
    public CutScene Cutscene;
    public UnityEvent OnCountDownFinished;



    //[SerializeField] private int NewSceneNumber = 1;


    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        Debug.Log("CS spillet: " + CutScenesPlayed);
        if(!timer_running)
        {
            return;
        }

        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime; //tr�kker den tid der er g�et fra den tid der er tilbage
        }

        if (remainingTime <= 4)// && remainingTime > 1)
        {
            countdownText.color = Color.yellow; //G�r teksten gul n�r der er under 4 (3) sekunder tilbage
        }

        if (remainingTime <= 1)
        {
            CutScenesPlayed = CutScenesPlayed + 1;
            OnCountDownFinished.Invoke();
            remainingTime = 0; //S�tter tiden til 0 s� timeren ikke kan blive negativ
            timer_running = false;
            countdownText.color = Color.red; //G�r teksten r�d
            //Debug.Log("Go to cutscene");
            //StartCoroutine(Cutscene.CutSceneInScene()); //kalder p� sceneskift funktionen fra cutscene scriptet
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60); //Omregner m�ngden af sekunder til minuter
        int seconds = Mathf.FloorToInt(remainingTime % 60); //Omregner m�ngden af tid til sekunder der er til rest efter minutterne
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //string format, g�r s� det ser p�nt ud

        if (CutScenesPlayed == TotalCutScenes)
        {
            StartCoroutine(Cutscene.FinalCutscene());
 
        }
    }


    public void StartTimer()
    {
        timer_running = true;
        remainingTime = CountdownTime;
    }
}


