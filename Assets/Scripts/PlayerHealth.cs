using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float currentHealthPoints = 100f;

    public void CastHeal()
    {
        currentHealthPoints += 30f;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints -= damage;
    }
}
