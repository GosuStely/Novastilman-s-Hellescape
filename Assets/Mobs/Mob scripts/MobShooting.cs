using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;

    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > 2) 
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    { 
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }
}
