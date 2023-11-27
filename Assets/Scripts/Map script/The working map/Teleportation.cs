using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 vector3 = transform.position;
        vector3.x += 80;
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            // Teleport the player to the destination
            other.transform.position = vector3;

            Vector3 mainCameraVector3 = new Vector3();
            mainCameraVector3 = Camera.main.transform.position;
            mainCameraVector3.x += 80;
            Camera.main.transform.position = mainCameraVector3;
        }
    }

}