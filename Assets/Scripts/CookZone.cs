using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookZone : MonoBehaviour
{
    public GameObject CookCam;
    public CinemachineFreeLook MainCam;
    public Transform CookPos;
    public Transform PlayerPos;
    public GameObject toolTip;
    public GameObject Line;
    public GameObject knife;
    public GameObject countdownText;
    public GameObject Player;
    public GameObject ratingMessage;
    private bool playerInTrigger = false;

    


    private void Start()
    {
        //Sluk for musen
        Cursor.visible = false;

        // Tjek om tegnescriptet findes
        Drawing drawing = Line.GetComponent<Drawing>();
        if (drawing != null)
        {
            //sluk tegne script
            drawing.enabled = false;
        }
        
        toolTip.SetActive(false); //Sluk tooltip
        CookCam.SetActive(false); //Sluk cookcam


        //kniven skal slukkes
        knife.SetActive(false);

        //CountdownText objektet slukkes (derved slukkes countdown scriptet p� det ogs�)
        countdownText.SetActive(false);


        Debug.Log("Message not hidden");
        ratingMessage.SetActive(false);
        Debug.Log("Message hidden");
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("Player"))
        {
            toolTip.SetActive(true);
            playerInTrigger = true;
            
        }
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and pressed the "E" key
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            toolTip.SetActive(false);
            CookCam.SetActive(true);
            MainCam.enabled = false;
            PlayerPos.position = CookPos.position;


            //kniven skal t�ndes
            knife.SetActive(true);

            //Tegne scriptet t�ndes
            Drawing drawing = Line.GetComponent<Drawing>();
            drawing.enabled = true;

            CountDownTimer countDown = Player.GetComponent<CountDownTimer>(); //hente countdowntimer scriptet fra spilleren
            countDown.enabled = true; //sl� den til mens man er i gang med at sk�re
            Debug.Log("the countdown timer script is now turned on");

        }

        if (playerInTrigger == false)
        {
            toolTip.SetActive(false);
            CookCam.SetActive(false);
            MainCam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger!");
            playerInTrigger = false;
            Drawing drawing = Line.GetComponent<Drawing>();
            drawing.enabled = false;

            //kniven skal slukkes
            knife.SetActive(false);

            CountDownTimer countDown = Player.GetComponent<CountDownTimer>(); //hente countdowntimer scriptet fra spilleren
            countDown.enabled = false; //stoppe scriptet n�r man forlader sk�re mode
            Debug.Log("the countdown timer script is now turned off");
        }
    }
}
