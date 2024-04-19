using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConvo : MonoBehaviour
{
    public AudioSource dialog;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
