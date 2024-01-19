using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class ItemAbility : MonoBehaviour
{
    private Health health;
    private PlayerMovement player;
    private DemonMovement demon;
    private RectTransform HPHolder;

    void Start() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        demon = GameObject.FindGameObjectWithTag("Mob").GetComponent<DemonMovement>();
        HPHolder = GameObject.FindGameObjectWithTag("HPHolder").GetComponent<RectTransform>();
    }

    public void Ring(int healthCount) {
        health.AddHealth(healthCount);
        Destroy(gameObject);
    }

    public void Boots(int speedCount) {
        player.playerSpeed = speedCount;
        Destroy(gameObject);
    }

    public void Bow(float damageCount) {
        player.playerDamage = damageCount;
        Destroy(gameObject);
    }

    public void Gloves(float dexterity) {
        player.fireDelay = dexterity;
        Destroy(gameObject);
    }

    public void Chestplate(int heartCount)
    {
        player.playerHitpoint += heartCount;
        Debug.Log("this logs" + player.playerHitpoint);

        for (int i = health.numberOfHearts; i <= player.playerHitpoint; i++)
        {
            health.hearts[i].gameObject.SetActive(true);
        }
      
        Destroy(gameObject);
        
    }

    public void Necklece(float time) {
        StartCoroutine(SlowWobblySpeed(time));
        Destroy(gameObject);
    }

    IEnumerator SlowWobblySpeed(float time) {
        demon.speed = 0.5f;
        yield return new WaitForSeconds(time);
        demon.speed = 3f;
    }
}
