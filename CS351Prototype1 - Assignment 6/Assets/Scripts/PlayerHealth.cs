using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public DisplayBar DisplayBar;
    private Rigidbody2D rb;
    public float knowchbackForce = 5f;
    public GameObject playerDeathEffect;
    public static bool hitRecently = false;
    public float hitRecoveryTime = 0.2f;

    private AudioSource playerAudio;
    public AudioClip playerHitSound;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.Log("Rigidbody2D not found on player.");
        }

        DisplayBar.SetMaxValue(health);
        hitRecently = false;
    }

    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
        {
            return;
        }
        hitRecently = true;

        if (gameObject.activeSelf)
        {
            StartCoroutine(RecoverFromHit());
        }

        Vector2 direction = transform.position - enemyPosition;

        direction.Normalize();

        direction.y = direction.y * 0.5f * 0.5f; 

        rb.AddForce(direction * knowchbackForce, ForceMode2D.Impulse);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime);

        hitRecently = false;

        animator.SetBool("hit", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        DisplayBar.SetValue(health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            playerAudio.PlayOneShot(playerHitSound);

            animator.SetBool("hit", true);
        }
    }

    public void Die()
    {
        ScoreManager.gameOver = true;

        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        Destroy(deathEffect, 2f);

        gameObject.SetActive(false);
    }
        // Update is called once per frame
     void Update()
    {
        
    }
}
