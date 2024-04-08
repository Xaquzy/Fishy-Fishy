using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WaterHose : MonoBehaviour
{
    public Transform hose;
    public Camera CookCam;
    public float DistFraKam = 1.5f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Grabpoint følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = DistFraKam;
        hose.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint

    }
}
