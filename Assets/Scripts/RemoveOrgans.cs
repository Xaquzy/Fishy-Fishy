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
    public CountDownTimer countDownTimer;
    
    //Animation
    public Animator HandAnimator;



    private void Start()
    {
    }
    private void Update()
    {
        // Grabpoint f�lger med musen
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
                if (Physics.Raycast(ray, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //der findes ikke down s� vi bruger minus op
                {
                    if (raycastHit.transform.TryGetComponent(out removableOrgan))
                    {
                        Debug.Log("H�nd IND");
                        HandAnimator.SetBool("Ud", false);
                        HandAnimator.SetBool("Ind", true);
                        removableOrgan.Grab(Hand);
          
                        if (countDownTimer.timer_running == false)
                        {
                            //countdown scriptet t�ndes;
                            Debug.Log("CountDownActive");
                            CountDownTimer countDownTimer = countdownText.GetComponent<CountDownTimer>(); //f� adgang til countdown script
                            countDownTimer.enabled = true;
                            countDownTimer.CountdownTime = TidTilAtGrabbeObj;
                            countDownTimer.StartTimer();
                            countdownText.SetActive(true);
                        }
                        

                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (removableOrgan != null)
            {
                Debug.Log("H�nd UD");
                HandAnimator.SetBool("Ind", false);
                HandAnimator.SetBool("Ud", true);
                removableOrgan.Drop();
                removableOrgan = null;
            }
        }
    }
}
