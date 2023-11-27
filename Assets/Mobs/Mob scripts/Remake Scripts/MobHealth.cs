using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    public int maxHealth = 0;
    public int currentHealth = 0;
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
            Debug.Log("Enemy died!");
        }
    }
}
