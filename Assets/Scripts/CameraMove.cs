using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    public float moveSpeed = 5f; // Speed at which the camera moves

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Check if there are waypoints
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to CameraPath script.");
            return;
        }

        // Move towards the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the camera has reached the current waypoint
        if (transform.position == currentWaypoint.position)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

