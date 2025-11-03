using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;

    private DisplayBar healthbar;

    public void Start()
    {
        healthbar = GetComponentInChildren<DisplayBar>();
        if (healthbar == null)
        {
            Debug.LogError("Healthbar not found on enemy");
            return;
        }
        healthbar.SetMaxValue(health);

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetValue(health);
        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
