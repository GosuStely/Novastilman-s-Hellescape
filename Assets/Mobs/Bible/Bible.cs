using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var healthComponent = other.GetComponent<PlayerMovement>();
            if (healthComponent != null)
            {
                healthComponent.GetHP(1);
            }
            Destroy(gameObject);
        }
    }
}
