using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager1 : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager1>();
        if (atm != null )
        {
            atm.TakeDamage(attack);
        }
    }

}
