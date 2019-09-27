using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    [HideInInspector ]public float velocity = 1;

    public float acceleration = 0.1f;
    private float mul = 1;
    public GameObject sparkParticle;

    private bool isActive = false;

    private Transform target;
    private Rigidbody2D rb;
    private Rigidbody2D targetrb;
    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Transform>();
        targetrb = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (isActive == true) { //When Spark has been tapped
            mul += acceleration;
            float step = (velocity * Time.deltaTime) * mul;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    //When Spark is tapped this method is executed from TouchDetection
    public void Activate() {    
        isActive = true;
        Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);
    }

}
