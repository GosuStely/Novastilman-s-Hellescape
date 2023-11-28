using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : PLAYERSTATS
{
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private string currentState;

    private bool isAttacking;

    public GameObject arrowPrefab;
    private float bulletSpeed = 10f;
    private float nextFire = 0.0f;
    private float fireDelay = 1f;
    public Transform firePoint; // firePoint 
    private float playerHitpoint = 100f;
    private float playerDamage = 3f;
    private float currentHP;

    // Animation states
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";

    void Start() {
        speed = SPEED;
        fireDelay = ATTACKSPEED;
        playerHitpoint = HP;
        playerDamage = DMG;
        currentHP = HP;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Capture input axes
        float moveHorizontal = 0;
        float moveVertical = 0;

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

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > nextFire && !isAttacking) {
            Shoot(shootHorizontal, shootVertical);
            animator.SetTrigger("Attack");
            nextFire = Time.time + fireDelay;
        }

        // Flip
        if (moveHorizontal < 0 && isFacingRight) {
            Flip();
        } else if (moveHorizontal > 0 && !isFacingRight) {
            Flip();
        }

        // Calculate movement direction
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Apply movement to the rigidbody
        rb.velocity = movement * speed;
    }

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
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.Euler(0, 0, (x > 0) ? 90 : 270));
        arrow.AddComponent<Rigidbody2D>().gravityScale = 0;
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed, 
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }
}
