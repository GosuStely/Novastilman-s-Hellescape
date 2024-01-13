using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCheckingScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float speed = 2f;
    private float distance;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (player.transform.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x);
        } else {
            scale.x = Mathf.Abs(scale.x) * -1;
        }

        transform.localScale = scale;

        if (distance < 10f) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        } else {
            animator.SetTrigger("DemonRun");
        }
    }
}
