using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    public float moveSpeed = 5f; 

    private int currentWaypointIndex = -1;
    private float distance;
    private Transform currentWaypoint;
    private Quaternion rotation;
    private Vector3 position;

    public GameObject subParent;
    public GameObject Sub1;
    public GameObject Sub2;
    public GameObject Sub3;
    public GameObject Sub4;
    public GameObject Sub5;
    public GameObject Sub6;
    private void Start()
    {
        NextPoint();

        subParent.SetActive(true);

        
        for (int i = 1; i < subParent.transform.childCount; i++)
        {
            Transform sub = subParent.transform.GetChild(i);
            sub.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        float distanceTravelled = (transform.position - position).magnitude;
        transform.rotation = Quaternion.Slerp(rotation, currentWaypoint.rotation, distanceTravelled / distance);

        // Check if the camera has reached the current waypoint
        if (transform.position == currentWaypoint.position)
        {
            // Move to the next waypoint
            NextPoint();
        }

        Debug.Log("Current position: " + transform.position);
        if (Vector3.Distance(transform.position, new Vector3(187.286362f, 204.089996f, 197.217484f)) < 0.03f)
        {
            Sub1.SetActive(false);
            Sub2.SetActive(true);
        }
        if (Vector3.Distance(transform.position, new Vector3(196.451462f, 204.089996f, 211.227127f)) < 0.03f)
        {
            Sub2.SetActive(false);
            Sub3.SetActive(true);
        }
        if (Vector3.Distance(transform.position, new Vector3(215.976974f, 204.089996f, 213.968185f))<0.03f)
        {
            Sub3.SetActive(false);
            Sub4.SetActive(true);
        }
        if (Vector3.Distance(transform.position, new Vector3(215.847168f, 204.089996f, 201.592606f)) < 0.03f)
        {
            Sub4.SetActive(false);
            Sub5.SetActive(true);
        }
        if (Vector3.Distance(transform.position, new Vector3(215.919952f, 204.089996f, 188.116257f)) < 0.03f)
        {
            Sub5.SetActive(false);
            Sub6.SetActive(true);
        }

    }
    

    void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        currentWaypoint = waypoints[currentWaypointIndex];
        distance = (currentWaypoint.position - transform.position).magnitude;
        rotation = transform.rotation;
        position = transform.position;
    }
}
    


