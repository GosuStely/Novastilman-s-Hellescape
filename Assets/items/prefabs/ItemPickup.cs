using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Powerup script triggered function 2");
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            
        }
    }
}
