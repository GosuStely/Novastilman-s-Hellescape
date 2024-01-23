using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;

    void Start() {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void BombEnable() {
        circleCollider2D.enabled = true;
    }

    public void BombDisable() {
        circleCollider2D.enabled = false;
    }
}
