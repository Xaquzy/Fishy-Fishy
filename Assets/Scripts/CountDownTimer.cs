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
            remainingTime = remainingTime - Time.deltaTime; //tr�kker den tid der er g�et fra den tid der er tilbage
        }

        if (remainingTime <= 4  && remainingTime > 1)
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
        yield return new WaitForSeconds(remainingTime + 1);
        Debug.Log("Go to cutscene");
        //SceneManager.LoadScene(NewSceneNumber);
    }
}
