using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkull : MonoBehaviour
{
    public Transform[] points;
    int current;

    private Rigidbody2D rb;
    private Animator anim;

    public float attackRadius;
    public float bulletForce = 20f;
    public float attackCoolDown = 0.5f;
    public float lastAttackTime;

    public LayerMask whatIsPlayer;

    public float speed;

    private bool isRunOutOfHP;

    [SerializeField] private int HP = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isDead", isRunOutOfHP);

        if (transform.position != points[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
        }
        else { 
            current = (current + 1) % points.Length;
        }
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);

            HP -= 1;
            FindObjectOfType<AudioManager>().Play("MobHit");
            if (HP <= 0)
            {
                speed = 0;
                isRunOutOfHP = true;
                anim.SetTrigger("isDead");
                FindObjectOfType<AudioManager>().Play("MonsterDeath");

                StartCoroutine(DestroyAfterDeath());
            }
        }
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
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
}
