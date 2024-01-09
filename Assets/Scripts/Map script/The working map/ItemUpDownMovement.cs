using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the movement
    public float height = 0.5f; // Height of the movement

    private Vector3 startPos;
    private float tempVal;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        tempVal = Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(startPos.x, startPos.y + tempVal, startPos.z);
    }
}
