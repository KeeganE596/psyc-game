using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt : MonoBehaviour
{

    public float velocity = 1;

    private Vector2 target;
    private Score_manager target_Score;


    private void Start() {
        target = new Vector2(0, 0);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);
    }

    public void despawn() {
        this.gameObject.SetActive(false);
    }
}
