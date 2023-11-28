using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (player.transform.rotation.y >= 0) {
            rb.velocity = Vector2.right * speed;
        } else if (player.transform.rotation.y <= -180) { // need to fix the arrow shooting to the left
            rb.velocity = Vector2.left * speed;
        }
        
        Destroy(gameObject, 2f);
    }

}
