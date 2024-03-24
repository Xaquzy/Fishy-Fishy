using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookZone : MonoBehaviour
{
    public GameObject CookCam;
    public CinemachineFreeLook MainCam;
    public Transform PlayerPos;
    public GameObject toolTip;
    public GameObject Line;
    public GameObject knife;
    public GameObject countdownText;
    public GameObject Player;
    public GameObject ratingMessage;
    private bool playerInTrigger = false;

    //kalde på countdowntimer scriptet
    public CountDownTimer CountDownTimer;
    //kalde på drawing script
    public Drawing Drawing;


    private void Start()
    {
        //Sluk for musen
        //Sluk for musen
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
        ratingMessage.SetActive(false); //sluk ratings





    //kniven skal slukkes
    knife.SetActive(false);

        //CountdownText objektet slukkes (derved slukkes countdown scriptet på det også)
        countdownText.SetActive(false);
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


            //kniven skal tændes
            knife.SetActive(true);

            //Tegne scriptet tændes
            //Drawing drawing = Line.GetComponent<Drawing>();
            Drawing.enabled = true;

            CountDownTimer.enabled = true; //slå den til mens man er i gang med at skære
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

            //CountDownTimer countDown = Player.GetComponent<CountDownTimer>(); //hente countdowntimer scriptet fra spilleren
            CountDownTimer.enabled = false; ; //stoppe scriptet når man forlader skære mode
            Debug.Log("the countdown timer script is now turned off");
        }
    }
}
