using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Eye_Movement : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public float bulletForce = 20f;
    public float attackCoolDown = 0.5f;
    public float lastAttackTime;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 direction;

    private bool isInChaseRange;
    private bool isInAttackRange;
    private bool isRunOutOfHP;

    [SerializeField] private int HP = 1;

    public List<LootItems> lootTable = new List<LootItems>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("isChasing", isInChaseRange);
        anim.SetBool("isAttacking", isInAttackRange);
        anim.SetBool("isDead", isRunOutOfHP);

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
            Attack();
        }

    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);

            HP -= 1;
            if (HP <= 0)
            {
                speed = 0;
                isRunOutOfHP = true;
                anim.SetTrigger("isDead");

                StartCoroutine(DestroyAfterDeath());
            }
            //FindObjectOfType<AudioManager>().Play("MobHit");
        }

        if (collision.tag == "Bomb")
        {

            HP -= 1;
            if (HP <= 0)
            {
                speed = 0;
                isRunOutOfHP = true;
                anim.SetTrigger("isDead");

                StartCoroutine(DestroyAfterDeath());
            }
            //FindObjectOfType<AudioManager>().Play("MobHit");
        }
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
        foreach (LootItems lootItem in lootTable)
        { 
            if(Random.Range(0f,100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
        }
    }

    void InstantiateLoot(GameObject loot) 
    {
        GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
    }
}
