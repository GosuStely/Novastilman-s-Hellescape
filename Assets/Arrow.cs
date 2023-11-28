using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PLAYERSTATS
{
    public float lifeTime = 1f;

    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
