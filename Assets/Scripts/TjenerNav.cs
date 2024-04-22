using UnityEngine;
using UnityEngine.AI;

public class TjenerNav : MonoBehaviour
{
    public Transform Counter; // Home location
    public Transform[] tableLocation; // List of locations to cycle through
    private NavMeshAgent agent;
    private int currentLocationIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (tableLocation.Length < 1)
        {
            Debug.LogError("At least one location is required for movement.");
            enabled = false;
        }
        GoToNextLocation();
    }
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if ((transform.position - Counter.position).magnitude <= agent.stoppingDistance)
            {
                //Hvis i k�kkenet.. g� ud og server
                GoToNextLocation();
            }
            else
            {
                //Ellers g� til k�kkenet
                ToCounter();
            }
        }
    }

    void GoToNextLocation()
    {
        agent.SetDestination(tableLocation[currentLocationIndex].position);
        currentLocationIndex = (currentLocationIndex + 1) % tableLocation.Length;
    }

    void ToCounter()
    {
        agent.SetDestination(Counter.position);
    }

}
