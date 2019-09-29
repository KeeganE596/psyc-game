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

    //private Transform target;
    private Vector2 target;
    private Rigidbody2D rb;
    private Rigidbody2D targetrb;
    
    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Score_Circle").GetComponent<Transform>();
        target = new Vector2(0, 0);
    }

    private void Update()
    {
        if (isActive == true) { //When Spark has been tapped
            mul += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, target, ((velocity * Time.deltaTime) * mul));
        }
    }

    //When Spark is tapped this method is executed from TouchDetection
    public void Activate() {    
        isActive = true;
        Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);
    }

}
