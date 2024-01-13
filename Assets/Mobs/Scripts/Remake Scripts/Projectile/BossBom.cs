using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBom : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var healthComponent = other.GetComponent<PlayerMovement>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(2);
            }

            // Destroy the bomb after dealing damage
            //DestroyAfterTouch();
        }
    }

    /*IEnumerator DestroyAfterTouch()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }*/

}

