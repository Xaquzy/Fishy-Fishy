using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemovableOrgan : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform GrabObjectPos;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        objectRigidBody.useGravity = false;
        objectRigidBody.constraints = RigidbodyConstraints.None; // slukker for freeze position. grunden til freeze position er tændt er grundetr æstetik hos organerne så det hele ikke bare falder alle steder i fisken
        this.GrabObjectPos = objectGrabPointTransform;
    }
    public void Drop()
    {
        this.GrabObjectPos = null;
        objectRigidBody.useGravity = true;
    }
    private void FixedUpdate()
    {
        if (GrabObjectPos != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, GrabObjectPos.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }
}
    