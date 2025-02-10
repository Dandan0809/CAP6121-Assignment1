using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
