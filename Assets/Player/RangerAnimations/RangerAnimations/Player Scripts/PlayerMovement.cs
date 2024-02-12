using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

        // shooting inputs: left, right, up and down arrow keys.
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > nextFire && !isAttacking) {
            // checking if the player's velocity (both vertical and horizontal) is equal to 0.
            if (moveHorizontal == 0 && moveVertical == 0) {
                Shoot(shootHorizontal, shootVertical);
                animator.SetTrigger("Attack");
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
}
