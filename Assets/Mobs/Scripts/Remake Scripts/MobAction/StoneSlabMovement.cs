using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSlabMovement : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float bulletForce = 20f;

    public bool shouldRotate;

    public float timeBtwShots;
    public float startTimeBtwShots;

    public LayerMask whatIsPlayer;
    //public Transform firePoint;
    public GameObject projectile;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    public Vector3 direction;

    private bool isInAttackRange;
    private bool isRunOutOfHP;

    [SerializeField] private int HP = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        anim.SetBool("isAttacking", isInAttackRange);
        anim.SetBool("isDead", isRunOutOfHP);

        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        if (shouldRotate)
        {
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
        }

    }

    private void FixedUpdate()
    {

        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
            if (timeBtwShots <= 0)
            {
                //Shoot();
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow" && isInAttackRange)
        {
            Destroy(collision.gameObject);

            HP -= 1;
            if (HP <= 0)
            {
                isRunOutOfHP = true;
                anim.SetTrigger("isDead");

                StartCoroutine(DestroyAfterDeath());
            }
        }
    }

    IEnumerator DestroyAfterDeath()
    { 
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
    //void Shoot()
    //{
    //  GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
    // Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    //}
}
