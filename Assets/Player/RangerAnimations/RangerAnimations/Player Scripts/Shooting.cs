using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrow;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Invoke("Shoot", 0.5f);
        }
    }

    void Shoot() {
        Instantiate(arrow, firePoint.position, Quaternion.Euler(new Vector3(0, 0, 90)));
    }
}
