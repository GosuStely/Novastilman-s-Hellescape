using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstance = this; 
    }
    void Start()
    {
        bullets = new List<GameObject>();
    }


    public GameObject GetBullet()
    { 
        if(bullets.Count > 0) //check if there are any inactive bullets in the pool
        {
            for (int i = 0; i < bullets.Count; i++) //iterate thru existing bullets
            {
                if (bullets[i] != null && !bullets[i].activeInHierarchy)
                { 
                    return bullets[i]; // if bullets inactive -> return for reuse
                }
            }
        }

        //if the pool runs low on bullets -> new bullets
        if(notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false); 
            bullets.Add(bul);
            return bul;
        }

        return null;

    }
    
}
