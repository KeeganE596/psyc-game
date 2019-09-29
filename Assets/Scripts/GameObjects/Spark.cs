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

    private Vector2 target;
    
    private void Start() {
        target = new Vector2(0, 0);
    }

    private void Update() {
        if (isActive == true) {
            mul += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, target, ((velocity * Time.deltaTime) * mul));
        }
    }

    //Activate Spark when it is clicked/tapped, called from Swipey_TouchDetection
    public void Activate() {    
        isActive = true;
        Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);
    }

}
