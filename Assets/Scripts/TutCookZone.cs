using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCookZone : MonoBehaviour
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

    //kalde på countdowntimer scriptet
    public CountDownTimer CountDownTimer;
    public SwapObjects SwapObjects;


    //kalde på alle egenskabsscripts og deres tilhørende objekter
    public Drawing Drawingscript;
    public GameObject knife;
    public GameObject Modelkniv;

    public RemoveOrgans RemoveOrgansscript;
    public GameObject Hand;

    public WaterHose WaterHosescript;
    public GameObject hose;
    public GameObject hoseModel;

    public AudioSource Part1;
    public AudioSource Part2;
    public AudioSource Part3;

    public GameObject pressParent;

    public Blood Blood;


    private void Start()
    {
        //Sluk for musen
        Cursor.visible = false;

        // Sluk egenskabsscriptne
        SlukEgenskaber();


        toolTip.SetActive(false); //Sluk tooltip
        CookCam.SetActive(false); //Sluk cookcam
        ratingMessage.SetActive(false); //sluk ratings

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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {

            InCookMode = true;
            Cursor.visible = true;
            toolTip.SetActive(false);
            CookCam.SetActive(true);
            MainCam.enabled = false;
            CountDownTimer.enabled = true; //slå den til mens man er i gang med din handling
            Debug.Log("the countdown timer script is now turned on");

            
        }

        if (InCookMode == true)
        {
            //Vælg egenskabsfunktion
            ChooseEgenskab();

            if (SwapObjects.currentIndex == 0)
            {
                Part1.Play();
                Debug.Log("Det er nu tid til at skære gutter");
                pressParent.transform.Find("Press2").gameObject.SetActive(true);
                
            }
            if (SwapObjects.currentIndex == 1)
            {
                Debug.Log("Det er nu tid til at vakse gutter");
                pressParent.transform.Find("Press3").gameObject.SetActive(true);
                Part2.Play();

            }
            // Check if Part2 has been played and BloodPool opacity is 0
            if (Part2.isPlaying && Blood.BloodPool.GetFloat("_Opacity") == 0)
            {
                Debug.Log("Det er nu tid til at grabbe gutter");
                pressParent.transform.Find("Press1").gameObject.SetActive(true);
                Part3.Play();
            }
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
            CountDownTimer.enabled = false; ; //stoppe scriptet når man forlader skære mode
            
            pressParent.transform.Find("Press1").gameObject.SetActive(false);
            pressParent.transform.Find("Press2").gameObject.SetActive(false);
            pressParent.transform.Find("Press3").gameObject.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Hvis man trykker på tallet 1
        {
            if (RemoveOrgansscript != null)
            {
                SlukEgenskaber(); //slukker alle og tænder en så man ikke har flere på samme tid

                //Tænd GrabObject script
                RemoveOrgansscript.enabled = true;
                Hand.SetActive(true);

                //Hosemodel
                hoseModel.SetActive(true);

                //kniv model
                Modelkniv.SetActive(true);


            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //Hvis man trykker på tallet 2
        {
            if (Drawingscript != null)
            {
                SlukEgenskaber(); //slukker alle og tænder en så man ikke har flere på samme tid

                //Tænd tegne script
                Drawingscript.enabled = true;
                knife.SetActive(true);
                Modelkniv.SetActive(false);

                //Hosemodel
                hoseModel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) //Hvis man trykker på tallet 3
        {
            if (WaterHosescript != null)
            {
                SlukEgenskaber(); //slukker alle og tænder en så man ikke har flere på samme tid

                //Tænd tegne script
                WaterHosescript.enabled = true;
                hose.SetActive(true);
                hoseModel.SetActive(false);

                //Kniv model
                Modelkniv.SetActive(true);
            }
        }
    }
}
