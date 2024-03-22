using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapObjects : MonoBehaviour

   
{
    public CountDownTimer countDownTimer;
    public List<GameObject> objectsToSwap = new List<GameObject>();
    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (objectsToSwap.Count < 2)
        {
            Debug.LogError("At least two objects are required to swap.");
            return;
        }

        // Enable første object i listen og diable resten
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == 0)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = countDownTimer.remainingTime;
        bool SwapFish = countDownTimer.SwapFish;

        if (remainingTime <=1 && SwapFish)
        {   
            SwapObjectsInList();
            countDownTimer.SwapFish = false;
        }
    }

    void SwapObjectsInList()
    {
        // Disable the current object
        objectsToSwap[currentIndex].SetActive(false);

        // Move to the next object in the list
        currentIndex = (currentIndex + 1) % objectsToSwap.Count;

        // Enable the next object
        objectsToSwap[currentIndex].SetActive(true);
    }
} 
