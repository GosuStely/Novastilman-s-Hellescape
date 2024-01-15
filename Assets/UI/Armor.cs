using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    private PlayerMovement player;
    private Health health;
    [SerializeField] private int numberOfArmor;
    [SerializeField] private Image[] armors;
    [SerializeField] private Sprite fullArmor;
    [SerializeField] private Sprite emptyArmor;

    void Start() {
        player = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        
        if (player.playerHitpoint > numberOfArmor) {
            player.playerHitpoint = numberOfArmor;
        }

        for (int i = 0; i < armors.Length; i++)
        {
            if (i < player.playerHitpoint) {
                armors[i].sprite = fullArmor;
            } else {
                armors[i].sprite = emptyArmor;
            }

            if (i < numberOfArmor) {
                armors[i].enabled = true;
            } else {
                armors[i].enabled = false;
            }
        }
    }
}

