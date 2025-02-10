using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
