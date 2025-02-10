using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDownEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>() != null)
        {
            other.GetComponent<EnemyAI>().KnockedDown();
        }
    }
}
