using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] waypoints; 
    public float moveSpeed = 5f;

    private int currentWaypointIndex = -1;
    private float distance;
    private Transform currentWaypoint;
    private Quaternion rotation;
    private Vector3 position;

    public GameObject subParent;
    public GameObject TutorialCam;
    public CinemachineFreeLook MainCam;
    private float timeElapsed = 0;

    private bool isMoving = true;


    private void Start()
    {
        timeElapsed = 0;
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
        timeElapsed = timeElapsed + Time.deltaTime;
        Debug.Log("Time Elapsed: " + timeElapsed);
        if (!isMoving)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        float distanceTravelled = (transform.position - position).magnitude;
        transform.rotation = Quaternion.Slerp(rotation, currentWaypoint.rotation, distanceTravelled / distance);

        // Check if the camera has reached the current waypoint
        if (transform.position == currentWaypoint.position)
        {
            NextPoint();
        }


        if (timeElapsed >= 7f)
        {
            subParent.transform.Find("Sub1").gameObject.SetActive(false);
            subParent.transform.Find("Sub2").gameObject.SetActive(true);
        }
        if (timeElapsed >= 12f)
        {
            subParent.transform.Find("Sub2").gameObject.SetActive(false);
            subParent.transform.Find("Sub3").gameObject.SetActive(true);
        }
        if (timeElapsed >= 19.5f)
        {
            subParent.transform.Find("Sub3").gameObject.SetActive(false);
            subParent.transform.Find("Sub4").gameObject.SetActive(true);
        }
        if (timeElapsed >= 26f)
        {
            subParent.transform.Find("Sub4").gameObject.SetActive(false);
            subParent.transform.Find("Sub5").gameObject.SetActive(true);
        }
        if (timeElapsed >= 31f)
        {
            subParent.transform.Find("Sub5").gameObject.SetActive(false);
            subParent.transform.Find("Sub6").gameObject.SetActive(true);
        }
        if (timeElapsed >= 33f)
        {
            isMoving = false; 
            StartCoroutine(OffLastSub());

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

    IEnumerator OffLastSub()
    {
        yield return new WaitForSeconds(2);
        subParent.transform.Find("Sub6").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        TutorialCam.SetActive(false);
    }
}