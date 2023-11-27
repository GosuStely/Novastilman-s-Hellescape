using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MobAction : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Rigidbody2D rb;

    private float coolDownTime;
    public float startTimeBtwShot;

    public GameObject bullet;
    public Transform player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        coolDownTime = startTimeBtwShot;
    }

    void Update()
    {
        //rotate to look at the player
        transform.LookAt(player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        float distanceToTarget = Vector2.Distance(transform.position, player.position);

        if (distanceToTarget > stoppingDistance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if(distanceToTarget < stoppingDistance) 
        { 
            transform.position = this.transform.position;
        }

        if (coolDownTime <= 0) //if CD time runs out, the enemy shoots new bullet
        {
            //spawn a bullet Instantiate (of the bullet, at the mob position, no rotation)
            Instantiate(bullet, transform.position, Quaternion.identity);
            coolDownTime = startTimeBtwShot; //if we dont do this, enemy will shoot every single frame
        }
        else
        { 
            coolDownTime -= Time.deltaTime; 
        }
    }
}
