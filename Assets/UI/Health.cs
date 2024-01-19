using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    private PlayerMovement player;
    [SerializeField] private int numberOfHearts;
    public Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    void Start() {
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player.playerHitpoint > numberOfHearts) {
            player.playerHitpoint = numberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.playerHitpoint) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
        if (player.playerHitpoint <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        GameOverScreen.Setup();
    }

    public void AddHealth(int amountOfHealth) {
        player.playerHitpoint += amountOfHealth;
    }
}
