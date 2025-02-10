using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float currentHealthPoints = 100f;

    public Image healthBar;
    public ParticleSystem healFX;

    public void CastHeal()
    {
        currentHealthPoints += 30f;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
        healthBar.fillAmount = currentHealthPoints / maxHealthPoints;
        healFX.Play();
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints -= damage;
        healthBar.fillAmount = currentHealthPoints / maxHealthPoints;
    }
}
