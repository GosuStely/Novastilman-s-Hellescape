using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : PLAYERSTATS
{
    [HideInInspector] public float playerSpeed; // used for inventory
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private string currentState;

    private bool isAttacking;
    private bool waiting;
    public GameObject arrowPrefab;
    public float bulletSpeed = 10f;
    private float nextFire = 0.0f;
    private float nextBomb = 0.0f;
    public float fireDelay = 0.5f; // used for inventory
    public float bombDelay = 1f;
    public Transform firePoint; // firePoint 
    public float playerHitpoint;

    [SerializeField] private GameObject bombPrefab;

    private SpriteRenderer sr;
    [SerializeField] private Material newMaterial;
    [SerializeField] private Material defaultMaterial;

    [HideInInspector] public float playerDamage = 3f; // used for inventory

    private Arrow arrow;

    // Animation states
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Walk";

    Vector2 movement;
    float moveHorizontal;
    float moveVertical;

    void Start() {
        playerSpeed = SPEED;
        fireDelay = ATTACKSPEED;
        playerHitpoint = HP;
        playerDamage = DMG;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAttacking = false;
        arrow = GetComponent<Arrow>();
        sr = GetComponent<SpriteRenderer>();

        if (gameObject == null) {
            Debug.LogError("Plaeyr is null!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Capture input axes
        moveHorizontal = 0;
        moveVertical = 0;

        // Checking input
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            moveHorizontal = Input.GetAxis("Horizontal");
            ChangeAnimationState(PLAYER_RUN);
        } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
            moveVertical = Input.GetAxis("Vertical");
            ChangeAnimationState(PLAYER_RUN);
        } else {
            ChangeAnimationState(PLAYER_IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextBomb) {
            var copy = Instantiate(bombPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y -0.85f, transform.position.z), Quaternion.identity);
            // StartCoroutine(BombTicking()); // bomb timer
            Destroy(copy, 1.3f);
            nextBomb = Time.time + bombDelay;
        }
        
        

        // shooting inputs: left, right, up and down arrow keys.
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > nextFire && !isAttacking) {

            // disable diagonal shooting
            if (shootHorizontal != 0) shootVertical = 0;
            else if (shootVertical != 0) shootHorizontal = 0;

            // checking if the player's velocity (both vertical and horizontal) is equal to 0.
            if (moveHorizontal == 0 && moveVertical == 0) {
                if (shootVertical > 0 && shootHorizontal == 0) {
                    firePoint.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
                    animator.SetTrigger("AttackUp");
                } else if (shootVertical < 0 && shootHorizontal == 0) {
                    firePoint.position = new Vector3(transform.position.x + 0.06f, transform.position.y - 2.76f, transform.position.z);
                    animator.SetTrigger("AttackDown");
                } else if (shootVertical == 0 && shootHorizontal > 0) {
                    firePoint.position = new Vector3(transform.position.x + 0.9f, transform.position.y - 0.78f, transform.position.z);
                    animator.SetTrigger("Attack");
                } else if (shootVertical == 0 && shootHorizontal < 0) {
                    firePoint.position = new Vector3(transform.position.x - 0.9f, transform.position.y - 0.78f, transform.position.z);
                    animator.SetTrigger("Attack");
                } 

                Shoot(shootHorizontal, shootVertical); // shoot!                
                nextFire = Time.time + fireDelay;
            }
        }

        // Flip
        if (moveHorizontal < 0 && isFacingRight) {
            Flip();
        } else if (moveHorizontal > 0 && !isFacingRight) {
            Flip();
        }

        // Calculate movement direction
        movement = new Vector2(moveHorizontal, moveVertical);

        // Apply movement to the rigidbody
        rb.velocity = movement * playerSpeed;
    }


    // bomb coroutine
    // IEnumerator BombTicking() {
    //     yield return new WaitForSecondsRealtime(2f);
    //     bombPrefab.gameObject.GetComponent<CircleCollider2D>().radius = 5f;
    //     yield return new WaitForSecondsRealtime(0.2f);
    //     bombPrefab.gameObject.GetComponent<CircleCollider2D>().radius = 0f;
    // }

    void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void Shoot(float x, float y) {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0, 0, (x > 0) ? 90 : 
        (y > 0) ? 180 : (y < 0) ? 0 : 270)); // if x > 0 = 90 degree, else if y > 0 = 180 degree, else if y < 0 = 0 degree and else if x < 0 = 270 degree.

        // checking if the player shoots to a right direction while being rotated to left, then the player flips itself.
        // the same logic goes if the player is facing to the right direction.
        if (Input.GetKey(KeyCode.LeftArrow) && isFacingRight) {
            Flip();
            isFacingRight = false;
        } else if (Input.GetKey(KeyCode.RightArrow) && !isFacingRight) {
            Flip();
            isFacingRight = true;
        }

        arrow.AddComponent<Rigidbody2D>().gravityScale = 0; // getting arrow physics component.
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed, // checking if x direction is on left, else: checking if x direction is on right and then applying force.
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed, // chceking y directions and applying force.
            0
        );
    }

    void Stop(float duration) {
        if (waiting) {
            return;
        }
        playerSpeed = 0f;
        sr.material = newMaterial;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration) {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        playerSpeed = SPEED;
        sr.material = defaultMaterial;
        waiting = false;
    }

    public void TakeDamage(int amount)
    {
        playerHitpoint -= amount;
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        Stop(0.1f);
        // animator.SetTrigger("PlayerHit");
        // stunning effect for a player
        if (playerHitpoint <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            //Add Death animation
            animator.SetTrigger("PlayerDeath");
            if (gameObject != null) {
                Destroy(gameObject, 0.5f);
            }
        }
    }

    public void GetHP(int amount)
    {
        playerHitpoint += amount;
    }
}
