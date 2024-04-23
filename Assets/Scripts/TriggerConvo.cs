using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConvo : MonoBehaviour
{
    public AudioSource dialog;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("Player"))
        {
            if (!dialog.isPlaying)
            {
                dialog.Play();
            }

         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialog.isPlaying)
            {
                dialog.Stop();
            }


        }
    }
}
