using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    public void Chestplate(int heartCount)
    {
        player.playerHitpoint += heartCount;
        Debug.Log("this logs" + player.playerHitpoint);

        for (int i = health.numberOfHearts; i <= player.playerHitpoint + heartCount; i++) // i = 5; i <= 5 + 1(bronze chestplate); i++
        {
            health.hearts[i].gameObject.SetActive(true);
      
        }
      
        Destroy(gameObject);
        
    }

    public void Helmet(int bulletCount) {
        player.bulletSpeed = bulletCount;
        Destroy(gameObject);
    }

    public void Necklece(float time) {
        StartCoroutine(SlowWobblySpeed(time));
        Destroy(gameObject);
    }

    IEnumerator SlowWobblySpeed(float time) {
        demon.speed = 0f;
        yield return new WaitForSeconds(time);
        demon.speed = 3f;
    }
}
