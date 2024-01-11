using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupil_Movement : MonoBehaviour
{
    private Transform player;
    public float followSpeed = 5f;

    public Transform eyeHole; // Reference to the hole in the tree

    private void Start()
    {
        // Assuming the eye hole is a child of the eye pupil
        eyeHole = transform.parent.Find("Eyehole");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null && eyeHole != null)
        {
            // Calculate the relative vector between the character and the eye pupil
            Vector3 relativeVector = player.position - eyeHole.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(relativeVector.y, relativeVector.x) * Mathf.Rad2Deg;

            // Rotate the eye pupil towards the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Move the eye pupil towards the player, but within the eye hole
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = ClampToEyeHole(transform.position, eyeHole.position, eyeHole.localScale.x / 1.5f);
        }
    }

    // Clamp the position to ensure the eye pupil stays within the eye hole
    private Vector3 ClampToEyeHole(Vector3 position, Vector3 eyeHolePosition, float eyeHoleRadius)
    {
        Vector3 offset = position - eyeHolePosition;
        offset = Vector3.ClampMagnitude(offset, eyeHoleRadius);
        return eyeHolePosition + offset;
    }
}
