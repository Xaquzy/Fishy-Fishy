using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookZone : MonoBehaviour
{
    public GameObject CookCam;
    public CinemachineFreeLook MainCam;
    public Transform CookPos;
    public Transform Player;
    public GameObject Text;
    public GameObject Line;
    public GameObject knife;
    private bool playerInTrigger = false;


    private void Start()
    {
        //Sluk for musen
        Cursor.visible = false;

        // Tjek om tegnescriptet findes
        Drawing drawing = Line.GetComponent<Drawing>();
        if (drawing != null)
        {
            //Tænd tegne script
            drawing.enabled = false;
        }

        Text.SetActive(false);
        CookCam.SetActive(false);
        
        //kniven skal slukkes
        knife.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("Player"))
        {
            Text.SetActive(true);
            playerInTrigger = true;

        }
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and pressed the "E" key
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            Text.SetActive(false);
            CookCam.SetActive(true);
            MainCam.enabled = false;

            Player.position = new Vector3(-2.98000002f, 1f, -25.0400009f);


            //kniven skal tændes
            knife.SetActive(true);

            Drawing drawing = Line.GetComponent<Drawing>();
            drawing.enabled = true;
        }

        if (playerInTrigger == false)
        {
            Text.SetActive(false);
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
        }
    }
}
