using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float bulletForce = 20f;

    public bool shouldRotate;

    public float timeBtwShots;
    public float startTimeBtwShots;

    public LayerMask whatIsPlayer;
    public Transform firePoint;
    public GameObject projectile;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 direction;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);
        anim.SetBool("isAttacking", isInAttackRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        if (shouldRotate)
        {
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);

        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
            if (timeBtwShots <= 0)
            {
                Shoot();
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void Shoot() 
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
