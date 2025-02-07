using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FutureVision : MonoBehaviour
{
    public GameObject locationPing;

    private void Start()
    {
        SeeEnemyPositions();
    }

    public void SeeEnemyPositions()
    {
        NavMeshAgent[] enemies = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        foreach (NavMeshAgent agent in enemies)
        {
            if (agent.destination != null)
            {
                GameObject ping = Instantiate(locationPing, agent.destination, Quaternion.identity);
                ping.GetComponent<PingBehavior>().AttachLine(agent);
            }
        }
    }
}
