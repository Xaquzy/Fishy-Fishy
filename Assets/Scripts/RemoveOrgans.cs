using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOrgans : MonoBehaviour
{
    [SerializeField] private Camera CookCam;
    [SerializeField] private Transform Hand;
    [SerializeField] float pickUpDistance = 10f;
    public float HandDistFraKam = 1.5f;
    [SerializeField] private LayerMask pickUpLayerMask;
    private RemovableOrgan removableOrgan;
    private void Update()
    {
        // Grabpoint følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = HandDistFraKam;
        Hand.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (removableOrgan == null)
            //Not carrying an object, try to grab
            {
                if (Physics.Raycast(Hand.position, -Hand.up, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) //der findes ikke down så vi bruger minus op
                {
                    if (raycastHit.transform.TryGetComponent(out removableOrgan))
                    {
                        removableOrgan.Grab(Hand);
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
