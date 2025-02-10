using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>() != null)
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
