using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MovePlayer(move);
    }

    void MovePlayer(Vector2 moveDirection)
    {
        // Normalize the direction to ensure consistent movement speed in all directions
        moveDirection.Normalize();

        // Calculate the new position based on the input and speed
        Vector3 newPosition = transform.position + new Vector3(moveDirection.x, moveDirection.y, 0f) * MoveSpeed * Time.deltaTime;

        // Move the player to the new position
        transform.position = newPosition;
    }
}
