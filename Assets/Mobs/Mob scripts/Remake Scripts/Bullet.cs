using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //target = position of the player when the bullet is spawned
        target = new Vector2(player.position.x, player.position.y);
        //player.position = (player.transform.position - this.transform.position).normalized;    
    }

    void Update()
    {
        //the bullet move to the player's position regardless of the player's presence
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //if we want to make it harder, and the bullet follow the player we can switch to this
        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //but then we have to make it disappear after a period of time of it will follow the player forever

        if(transform.position.x == target.x && transform.position.y == target.y) 
        {
            
            DestroyBullet();
        }
    }

    //this makes the bullet disappears when it touches the player's or the wall collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            DestroyBullet();
        }

        if (other.CompareTag("Player"))
        {
            var healthComponent = other.GetComponent<PlayerHealth>();
            if (healthComponent != null) 
            {
                healthComponent.TakeDamage(1);
            }
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }


}
