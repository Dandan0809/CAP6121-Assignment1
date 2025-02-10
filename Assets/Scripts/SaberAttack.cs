using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
