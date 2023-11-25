using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 vector3 = new Vector3(x, y, z);
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            // Teleport the player to the destination
            other.transform.position = vector3;
        }
    }
}