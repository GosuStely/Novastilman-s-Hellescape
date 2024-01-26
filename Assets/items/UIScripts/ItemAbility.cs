using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemAbility : MonoBehaviour
{
    private Health health;
    private PlayerMovement player;
    private DemonMovement demon;
    private Bomb bomb;

    void Start() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        demon = GameObject.FindGameObjectWithTag("Mob").GetComponent<DemonMovement>();
        bomb = GameObject.FindGameObjectWithTag("Bomb").GetComponent<Bomb>();
    }

    public void Ring(int healthCount) {
        health.AddHealth(healthCount);
        FindObjectOfType<AudioManager>().Play("PowerUp");
        Destroy(gameObject);
    }

    public void Boots(int speedCount) {
        player.playerSpeed = speedCount;
        FindObjectOfType<AudioManager>().Play("PowerUp");
        Destroy(gameObject);
    }

    public void Bow(float damageCount) {
        player.playerDamage = damageCount;
        FindObjectOfType<AudioManager>().Play("PowerUp");
        Destroy(gameObject);
    }

    public void Gloves(float dexterity) {
        player.fireDelay = dexterity;
        FindObjectOfType<AudioManager>().Play("PowerUp");
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

        FindObjectOfType<AudioManager>().Play("PowerUp");
      
        Destroy(gameObject);
        
    }

    public void Shield(float radius) {
        FindObjectOfType<AudioManager>().Play("PowerUp");
        bomb.GetComponent<CircleCollider2D>().radius = radius;
        Destroy(gameObject);
    }

    public void Helmet(int bulletCount) {
        player.bulletSpeed = bulletCount;
        FindObjectOfType<AudioManager>().Play("PowerUp");
        Destroy(gameObject);
    }

    public void Necklece(float time) {
        StartCoroutine(SlowWobblySpeed(time));
        FindObjectOfType<AudioManager>().Play("PowerUp");
        Destroy(gameObject);
    }

    IEnumerator SlowWobblySpeed(float time) {
        demon.speed = 0f;
        yield return new WaitForSeconds(time);
        demon.speed = 3f;
    }
}
