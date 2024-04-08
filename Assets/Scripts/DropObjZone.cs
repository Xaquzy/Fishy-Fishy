using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjZone : MonoBehaviour
{
    public int AmountToMoveOn;
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("GrabObj"))
        {
            AmountToMoveOn = AmountToMoveOn - 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("GrabObj"))
        {
            AmountToMoveOn = AmountToMoveOn + 1;
        }
    }

    private void Update()
    {
        if (AmountToMoveOn == 0) 
        {
            //Call cutscene
        }
    }
}
