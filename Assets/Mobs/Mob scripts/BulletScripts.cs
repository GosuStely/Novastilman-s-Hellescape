using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private float force;
    private int damage; // Use this variable to store the attack value

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        SetBulletDirection();
    }

    public void SetDamageValue(int attackValue)
    {
        damage = attackValue;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player
            AttributesManager1 playerAttributes = other.GetComponent<AttributesManager1>();
            if (playerAttributes != null)
            {
                playerAttributes.TakeDamage(damage);
            }

            // Destroy the bullet when it hits the player
            Destroy(gameObject);
        }
    }

    private void SetBulletDirection()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
