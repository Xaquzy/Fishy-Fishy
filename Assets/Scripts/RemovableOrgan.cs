using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemovableOrgan : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        objectRigidBody.constraints = RigidbodyConstraints.None; // slukker for freeze position. grunden til freeze position er tændt er grundetr æstetik hos organerne så det hele ikke bare falder alle steder i fisken
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;
    }
    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position,
            objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }
}
