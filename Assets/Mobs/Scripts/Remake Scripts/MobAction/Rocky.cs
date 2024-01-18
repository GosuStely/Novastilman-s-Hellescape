using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocky : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public float attackRadius;
    public float bulletForce = 20f;
    public float attackCoolDown = 0.5f;
    public float lastAttackTime;

    public LayerMask whatIsPlayer;

    public float speed;

    [SerializeField] private int HP = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRolling", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2 (-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCoolDown)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsPlayer);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    var healthComponent = collider.GetComponent<PlayerMovement>();
                    if (healthComponent != null)
                    {
                        healthComponent.TakeDamage(1);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            var healthComponent = other.GetComponent<PlayerMovement>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }
        }*/

        if (other.tag == "Arrow")
        {
            Destroy(other.gameObject);

            HP -= 1;
            if (HP <= 0)
            {
                speed = 0;

                anim.SetTrigger("isDead");

                StartCoroutine(DestroyAfterDeath());
            }
        }
    }
    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

}
