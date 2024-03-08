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
    private bool playerInTrigger = false;


    private void Start()
    {
        Text.SetActive(false);
        CookCam.SetActive(false);
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
            Text.SetActive(false);
            CookCam.SetActive(true);
            MainCam.enabled = false;
            Player.position = CookPos.position;
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
        }
    }
}
