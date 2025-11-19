using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Projectile class that controls the movement of the projectile class
//Attach this script to the projectile prefab
public class Projectile : MonoBehaviour
{
    //Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    //Speed of the projectile
    public float speed = 20f;

    //Damage of the projectile with a default value of 20
    public int damage = 20;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Check if the projectile hits an enemy
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        if (hitInfo.gameObject.tag != "Player")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
