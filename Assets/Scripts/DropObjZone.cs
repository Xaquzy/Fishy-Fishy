using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjZone : MonoBehaviour
{
    public List<int> AmountToMoveOn = new List<int>();
    [HideInInspector] public int AmountToMoveOnIndex = 0;
    public int ZoneScore;

    public void Start()
    {
        ZoneScore = AmountToMoveOn[AmountToMoveOnIndex];
    }

    public void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabObj"))
        {
            ZoneScore = ZoneScore - 1;
        }
    }
}
