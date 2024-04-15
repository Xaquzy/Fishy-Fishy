using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class RemoveOrgans : MonoBehaviour
{
    [SerializeField] private Camera CookCam;
    [SerializeField] private Transform Hand;
    [SerializeField] float pickUpDistance = 10f;
    public float HandDistFraKam = 1.5f;
    [SerializeField] private LayerMask pickUpLayerMask;
    private RemovableOrgan removableOrgan;
    public GameObject countdownText;
    public float TidTilAtGrabbeObj = 10f;
    private bool timerStarted;

    private void Start()
    {
        timerStarted = false;
    }
    private void Update()
    {
        // Grabpoint følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = HandDistFraKam;
        Hand.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint
        
        //Ray til raycast pickupdrop
        Ray ray = CookCam.ScreenPointToRay(MousePos);
        Debug.DrawRay(ray.origin, ray.direction, Color.cyan);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (removableOrgan == null)
            //Not carrying an object, try to grab
            {
                if (Physics.Raycast(ray, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //der findes ikke down så vi bruger minus op
                {
                    if (raycastHit.transform.TryGetComponent(out removableOrgan))
                    {
                        removableOrgan.Grab(Hand);

                        if (!timerStarted)
                        {
                            //countdown scriptet tændes;
                            CountDownTimer countDownTimer = countdownText.GetComponent<CountDownTimer>(); //få adgang til countdown script
                            countDownTimer.enabled = true;
                            countDownTimer.CountdownTime = TidTilAtGrabbeObj;
                            countDownTimer.StartTimer();
                            countdownText.SetActive(true);
                            timerStarted = true;
                        }
                        

                    }
                }
            }
            
            else
            {
                removableOrgan.Drop();
                removableOrgan = null;
            }
        }
    }
}
