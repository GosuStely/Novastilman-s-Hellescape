using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private string currentState;

    private bool isAttacking;

    // Animation states
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_ATTACK = "PlayerAttack";

    void Start() {
        speed = speed;
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

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) {
            // ShootArrow();
            animator.SetTrigger("Attack");
            
        }

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

    void ShootArrow() {

    }
}
