using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Update()
    {
        // Grabpoint følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = HandDistFraKam;
        Hand.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint

        //Ray til raycast pickupdrop
        Ray ray = CookCam.ScreenPointToRay(MousePos);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (removableOrgan == null)
            //Not carrying an object, try to grab
            {

                if (Physics.Raycast(ray, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //der findes ikke down så vi bruger minus op
                {
                    Debug.Log("hit");
                    if (raycastHit.transform.TryGetComponent(out removableOrgan))
                    {
                        Debug.Log("Object hit: " + raycastHit.transform.name); // Debug log når et correct object er hit
                        removableOrgan.Grab(Hand);

                        //countdown scriptet tændes
                        CountDownTimer countDownTimer = countdownText.GetComponent<CountDownTimer>(); //få adgang til countdown script
                        countDownTimer.enabled = true;

                        //Timeren startes når en linje tegnes
                        countDownTimer.StartTimer();

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
