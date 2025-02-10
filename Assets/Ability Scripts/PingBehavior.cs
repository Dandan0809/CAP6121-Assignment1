using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PingBehavior : MonoBehaviour
{
    public NavMeshAgent enemy;
    public LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void AttachLine(NavMeshAgent target)
    {
        enemy = target;
        line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        StartCoroutine(DisplayLine());
    }

    public IEnumerator DisplayLine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < 5f) // Loop until 5 seconds have passed
        {
            line.SetPosition(1, new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z));
            if (enemy.pathEndPosition == enemy.transform.position || enemy == null)
            {
                Destroy(gameObject);
            }
            float Distance = Vector3.Distance(enemy.transform.position, transform.position);
            yield return null; // Wait for the next frame
        }
        Destroy(gameObject);
    }
}
