using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 1.5f;
    public float attackCD = 2f;

    private Rigidbody2D rb;
    private float lastAttackTime;

    public AttributesManager1 playerAtm;
    public AttributesManager1 enemyAtm;

    public GameObject bullet;
    public Transform bulletPosition;
    private GameObject bulletInstance;

    private float timer;

    private bool isPlayerInRoom;

   
    public void SetPlayerInRoom(bool value)
    {
        isPlayerInRoom = value;

        if (isPlayerInRoom)
        {
            Debug.Log("Player entered the room!");
        }
        else if (!isPlayerInRoom)
        {
            Debug.Log("Player exited the room!");
        }
    }

    private void Awake()
    {
        SetPlayerInRoom(false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Ensure the enemy does not rotate due to physics forces
        lastAttackTime = -attackCD; // Start with a cooldown to allow the first attack immediately
    }

    void Update()
    {
        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > 10f)
        {
            //transform.Translate(new Vector3(0, 0, 0));
            rb.velocity = Vector2.zero;
            //Debug.Log("Idle");
        }
        else if (distanceToTarget > 2f && distanceToTarget <= 10f)
        {
            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0)); //move towards the player
            rb.velocity = (target.position - transform.position).normalized * speed;
            //Debug.Log("Chase");

        }
        else if (distanceToTarget <= 1.5f && Time.time - lastAttackTime > attackCD)
        {
            timer += Time.deltaTime;

            //transform.Translate(new Vector3(0, 0, 0)); 
            rb.velocity = Vector3.zero;
            //Debug.Log("Attack!");
            timer = 0f;
            Shoot();

            //attack and decrease the player's health
            enemyAtm.DealDamage(playerAtm.gameObject);

            lastAttackTime = Time.time;
            Debug.Log("CD");
        }
    }

    void Shoot()
    {
        bulletInstance = Instantiate(bullet, bulletPosition.position, Quaternion.identity);

        // Pass the attack value to the bullet
        BulletScripts bulletScript = bulletInstance.GetComponent<BulletScripts>();
        if (bulletScript != null)
        {
            bulletScript.SetDamageValue(playerAtm.attack);
        }
    }
}
