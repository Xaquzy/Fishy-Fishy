using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawLine : MonoBehaviour

    
{
    private LineRenderer line;
    private Vector3 previousPosition;
    public float minDistance;


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        previousPosition = transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            
            if(Vector3.Distance(currentPosition,previousPosition) > minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentPosition);
                previousPosition = currentPosition;
            }
        }
    }
}
