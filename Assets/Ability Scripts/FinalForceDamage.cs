using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalForceDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>() != null && other.GetComponent<EnemyAI>().isBoss == false)
        {
            Destroy(other.gameObject);
        }
    }
}
