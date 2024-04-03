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
    private bool timer_running = false;

    //Henter cutscene scriptet
    public CutScene Cutscene;
    public UnityEvent OnCountDownFinished;


    public bool SwapFish = false;

    //[SerializeField] private int NewSceneNumber = 1;


    void Start()
    {
        GameObject ratingMessage = Cutscene.ratingMessage;
        Debug.Log("Rating er på");
        ratingMessage.SetActive(false);
        Debug.Log("Rating er ikke på");
        StartTimer();
    }

    void Update()
    {
        if(!timer_running)
        {
            return;
        }

        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime; //trækker den tid der er gået fra den tid der er tilbage
        }

        if (remainingTime <= 4)// && remainingTime > 1)
        {
            countdownText.color = Color.yellow; //Gør teksten gul når der er under 4 (3) sekunder tilbage
        }

        if (remainingTime <= 1)
        {
            OnCountDownFinished.Invoke();
            remainingTime = 0; //Sætter tiden til 0 så timeren ikke kan blive negativ
            timer_running = false;
            countdownText.color = Color.red; //Gør teksten rød
            //Debug.Log("Go to cutscene");
            //StartCoroutine(Cutscene.CutSceneInScene()); //kalder på sceneskift funktionen fra cutscene scriptet
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60); //Omregner mængden af sekunder til minuter
        int seconds = Mathf.FloorToInt(remainingTime % 60); //Omregner mængden af tid til sekunder der er til rest efter minutterne
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //string format, gør så det ser pænt ud

    }


    public void StartTimer()
    {
        timer_running = true;
        remainingTime = CountdownTime;
    }
}


