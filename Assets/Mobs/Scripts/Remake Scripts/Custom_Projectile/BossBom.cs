using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBom : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            var healthComponent = other.GetComponent<PlayerMovement>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(2);
            }
        }
    }

}
