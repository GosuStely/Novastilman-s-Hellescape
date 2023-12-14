using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    { 
        currentHealth -= amount;
        if (currentHealth <= 0) 
        {
            //Add Death animation
            Debug.Log("You died!");
        }
    }
}
