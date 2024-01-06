using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (Vector2)player.position;

        // Calculate the direction towards the player
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();

        // Set the initial velocity of the bullet to move towards the player
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collider belongs to the player or a wall
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            // Destroy the bullet when it touches the player or a wall
            Destroy(gameObject);
        }
        //if (other.CompareTag("Player"))
        //{
            //var healthComponent = other.GetComponent<PlayerCollisionWithMob>();
            //if (healthComponent != null)
            //{
                //healthComponent.TakeDamage(1);
            //}
        //}
    }
}

