using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    public float remainingTime = 5f;

    //Cutscene ting
    //[SerializeField] private int NewSceneNumber = 1;

    void Update()
    {


        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime; //trækker den tid der er gået fra den tid der er tilbage
        }

        if (remainingTime <= 4  && remainingTime > 1)
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
        yield return new WaitForSeconds(remainingTime + 1);
        Debug.Log("Go to cutscene");
        //SceneManager.LoadScene(NewSceneNumber);
    }
}
