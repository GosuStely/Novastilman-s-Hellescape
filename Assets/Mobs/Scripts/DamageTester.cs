using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DamageTester : MonoBehaviour
{
    public AttributesManager1 playerAtm;
    public AttributesManager1 enemyAtm;
    private void Update()
    {
        //deal player dmg to the enemy health
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAtm.DealDamage(enemyAtm.gameObject);
        }

       
    }
}
