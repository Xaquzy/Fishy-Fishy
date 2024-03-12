using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOrgans : MonoBehaviour
{
    [SerializeField] private Transform CookCam;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] float pickUpDistance = 2.0f;
    [SerializeField] private LayerMask pickUpLayerMask;
    private RemovableOrgan removableOrgan;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (removableOrgan == null)
            //Not carrying an object, try to grab
            {
                //Eye Height
                if (Physics.Raycast(CookCam.position, CookCam.forward, out
               RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out removableOrgan))
                    {
                        removableOrgan.Grab(objectGrabPointTransform);
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
