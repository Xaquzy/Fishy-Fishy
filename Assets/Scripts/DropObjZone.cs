using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjZone : MonoBehaviour
{
    public int AmountToMoveOn;
    [HideInInspector] public int ZoneScore;

    public void Start()
    {
        ZoneScore = AmountToMoveOn;
    }

    public void Update()
    {
        if (ZoneScore <= 0)
        {
            ZoneScore = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has a specific tag
        if (other.CompareTag("GrabObj"))
        {
            ZoneScore = ZoneScore - 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("GrabObj"))
        {
            ZoneScore = ZoneScore + 1;
        }
    }
}
