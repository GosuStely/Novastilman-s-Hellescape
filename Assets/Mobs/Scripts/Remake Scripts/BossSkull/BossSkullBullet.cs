using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkullBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        speed = 5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (Vector2)player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    { 
        moveDirection = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            var healthComponent = other.GetComponent<PlayerMovement>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Wall"))
        {
            // Destroy the bullet when it touches a wall
            gameObject.SetActive(false);
        }


    }
}
