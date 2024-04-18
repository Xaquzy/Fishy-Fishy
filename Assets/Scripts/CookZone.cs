using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookZone : MonoBehaviour
{
    public GameObject CookCam;
    public CinemachineFreeLook MainCam;
    public Transform CookPos;
    public GameObject toolTip;
    public GameObject Line;
    public GameObject countdownText;
    public GameObject Player;
    public GameObject ratingMessage;
    private bool playerInTrigger = false;
    private bool InCookMode = false;

    //kalde p� countdowntimer scriptet
    public CountDownTimer CountDownTimer;


    //kalde p� alle egenskabsscripts og deres tilh�rende objekter
    public Drawing Drawingscript;
    public GameObject knife;
    public GameObject Modelkniv;

    public RemoveOrgans RemoveOrgansscript;
    public GameObject Hand;

    public WaterHose WaterHosescript;
    public GameObject hose;
    public GameObject hoseModel;



    private void Start()
    {
        //Sluk for musen
        Cursor.visible = false;

        // Sluk egenskabsscriptne
        SlukEgenskaber();


        toolTip.SetActive(false); //Sluk tooltip
        CookCam.SetActive(false); //Sluk cookcam
        ratingMessage.SetActive(false); //sluk ratings

        //CountdownText objektet slukkes (derved slukkes countdown scriptet p� det ogs�)
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            InCookMode = true;
            Cursor.visible = true;
            toolTip.SetActive(false);
            CookCam.SetActive(true);
            MainCam.enabled = false;
            CountDownTimer.enabled = true; //sl� den til mens man er i gang med din handling
            Debug.Log("the countdown timer script is now turned on");

            

        }

        if (InCookMode == true)
        {
            //V�lg egenskabsfunktion
            ChooseEgenskab();
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
            InCookMode = false;
            //Drawing drawing = Line.GetComponent<Drawing>();
            Drawingscript.enabled = false;

            //Objekterne der er vigtige til egenskaberne skal 
            knife.SetActive(false);
            hose.SetActive(false);
            Hand.SetActive(false);

            //CountDownTimer countDown = Player.GetComponent<CountDownTimer>(); //hente countdowntimer scriptet fra spilleren
            CountDownTimer.enabled = false; ; //stoppe scriptet n�r man forlader sk�re mode
            Debug.Log("the countdown timer script is now turned off");
        }
    }

    public void SlukEgenskaber()
    {
        if (Drawingscript != null)
        {
            //sluk tegne script
            Drawingscript.enabled = false;
            knife.SetActive(false);
        }

        if (RemoveOrgansscript != null)
        {
            //sluk GrabObject script
            RemoveOrgansscript.enabled = false;
            Hand.SetActive(false);
        }

        if (WaterHosescript != null)
        {
            //Sluk vand
            WaterHosescript.enabled = false;
            hose.SetActive(false);
        }
    }

    void ChooseEgenskab()
    {
        //Lav et tool tip der viser hvilket tal der svarer til hvilken egenskab
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Hvis man trykker p� tallet 1
        {
            if (RemoveOrgansscript != null)
            {
                SlukEgenskaber(); //slukker alle og t�nder en s� man ikke har flere p� samme tid

                //T�nd GrabObject script
                RemoveOrgansscript.enabled = true;
                Hand.SetActive(true);

                //Hosemodel
                hoseModel.SetActive(true);

                //kniv model
                Modelkniv.SetActive(true);


            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //Hvis man trykker p� tallet 2
        {
            if (Drawingscript != null)
            {
                SlukEgenskaber(); //slukker alle og t�nder en s� man ikke har flere p� samme tid

                //T�nd tegne script
                Drawingscript.enabled = true;
                knife.SetActive(true);
                Modelkniv.SetActive(false);

                //Hosemodel
                hoseModel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) //Hvis man trykker p� tallet 3
        {
            if (WaterHosescript != null)
            {
                SlukEgenskaber(); //slukker alle og t�nder en s� man ikke har flere p� samme tid

                //T�nd tegne script
                WaterHosescript.enabled = true;
                hose.SetActive(true);
                hoseModel.SetActive(false);

                //Kniv model
                Modelkniv.SetActive(true);
            }
        }
    }
}
