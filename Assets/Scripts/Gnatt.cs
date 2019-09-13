using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt : MonoBehaviour
{

    public float velocity = 1;

    private Transform target;
    private Rigidbody2D rb;
    private Rigidbody2D targetrb;
    private Score_manager target_Score;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Transform>();
        targetrb = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Rigidbody2D>();
        target_Score = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Score_manager>();

        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);

        if (target_Score.score >= 200) {
            velocity = 0;
        }
    }

    public void despawn() {
        Destroy(gameObject);
    }
}
