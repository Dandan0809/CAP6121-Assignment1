using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FutureVision : MonoBehaviour
{
    public GameObject locationPing;
    public bool onCooldown = false;

    public void SeeEnemyPositions()
    {
        if (onCooldown)
        {
            return;
        }
        onCooldown = true;
        StartCoroutine(CountCooldown());
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

    private IEnumerator CountCooldown()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(4f);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(10f);
    }

}
