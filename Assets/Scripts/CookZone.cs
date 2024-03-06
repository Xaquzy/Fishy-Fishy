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
            CookCam.SetActive(false);
            MainCam.enabled = false;
            Player.position = new Vector3(-0.531924486f, -0.178946912f, 2.714468f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger!");
        }
    }
}
