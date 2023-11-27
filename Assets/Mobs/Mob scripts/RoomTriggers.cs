using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("FloorLayer"))
        {
            MobMovement mobBehaviors = other.GetComponent<MobMovement>();

            if (mobBehaviors != null)
            {
                mobBehaviors.SetPlayerInRoom(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("FloorLayer"))
        {
            MobMovement mobBehaviors = other.GetComponent<MobMovement>();

            if (mobBehaviors != null)
            {
                mobBehaviors.SetPlayerInRoom(false);
            }
        }
    }
}
