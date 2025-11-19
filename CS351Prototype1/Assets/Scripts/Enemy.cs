using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;

    private DisplayBar healthbar;

    public int damage = 10;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on player.");
                return;
            }
            playerHealth.TakeDamage(damage);
            playerHealth.Knockback(transform.position);
        }
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
