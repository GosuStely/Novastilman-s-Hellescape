using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAbility : MonoBehaviour
{
    private Health health;
    private PlayerMovement player;
    private DemonMovement demon;

    void Start() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        demon = GameObject.FindGameObjectWithTag("Mob").GetComponent<DemonMovement>();
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
