using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [SerializeField] private HealthColour healthColour;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthColour();

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        UpdateHealthColour();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        UpdateHealthColour();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void UpdateHealthColour()
    {
        if (healthColour != null)
        {
            healthColour.UpdateHealth(currentHealth);
        }
    }
}
