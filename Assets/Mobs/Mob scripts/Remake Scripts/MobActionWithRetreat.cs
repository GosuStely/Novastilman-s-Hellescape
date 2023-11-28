using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobActionWithRetreat : MonoBehaviour
{
    private float currentSpeed;
    public float defaultSpeed;
    public float stoppingDistance;
    public float retreatDistance;

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
        float distanceToTarget = Vector2.Distance(transform.position, player.position);

        if (distanceToTarget > 15)
        {
            Idle();
        }        
        if (distanceToTarget > stoppingDistance && distanceToTarget <= 15)
        {
            Attack();
            SpawnBullet();
        }
        else if (distanceToTarget < stoppingDistance && distanceToTarget > retreatDistance)
        {
            StopChasing();
            Attack();
            SpawnBullet();
        }
        else if (distanceToTarget < retreatDistance)
        {
            Retreat();
            SpawnBullet();
        }


        
    }

    void Idle()
    {
        currentSpeed = 0f;
    }

    void Attack()
    {
        currentSpeed = defaultSpeed;
        transform.LookAt(player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        transform.position = Vector2.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
    }

    void StopChasing()
    {
        transform.LookAt(player.position);
    }

    void Retreat()
    {
        currentSpeed = defaultSpeed;
        transform.LookAt(player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        transform.position = Vector2.MoveTowards(transform.position, player.position, -currentSpeed * Time.deltaTime);
    }

    void SpawnBullet()
    {
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
