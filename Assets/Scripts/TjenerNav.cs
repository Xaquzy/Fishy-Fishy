using UnityEngine;
using UnityEngine.AI;

public class TjenerNav : MonoBehaviour
{
    public Transform[] tableLocations; // Array of table locations
    public Transform counterLocation; // Counter location
    public float servingTime = 3f; // Time taken to serve food
    public float waitTimeAtTable = 5f; // Time to wait at each table before returning to the counter
    public float waitTimeAtCounter = 2f; // Time to wait at the counter before going to the next table

    private NavMeshAgent Tjener;
    private int currentTableIndex = 0;
    private bool isAtCounter = true; // Initially, the server is at the counter

    private void Start()
    {
        Tjener = GetComponent<NavMeshAgent>();
        ReturnToCounter();
    }

    private void Update()
    {
        if (Tjener.remainingDistance < 0.1f && !Tjener.pathPending)
        {
            if (isAtCounter)
            {
            }
            else
            {
                ReturnToCounter();
            }
        }
    }

    private void GoToNextTable()
    {
        if (tableLocations.Length == 0)
        {
            Debug.LogError("No table locations assigned!");
            return;
        }

        if (currentTableIndex >= tableLocations.Length)
        {
            currentTableIndex = 0; // Reset to the first table
        }

        Tjener.SetDestination(tableLocations[currentTableIndex].position);
        isAtCounter = false;
    }


    private void ReturnToCounter()
    {
        Tjener.SetDestination(counterLocation.position);
    }
}
