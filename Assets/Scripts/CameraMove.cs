using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    public float moveSpeed = 5f; 

    private int currentWaypointIndex = 0;

    void Update()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, currentWaypoint.rotation, moveSpeed * Time.deltaTime);

        // Check if the camera has reached the current waypoint
        if (transform.position == currentWaypoint.position ||transform.rotation==currentWaypoint.rotation)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

