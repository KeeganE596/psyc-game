using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnatt : MonoBehaviour
{
    public float velocity = 1;

    private Vector2 target;

    bool doDespawn;
    float timer;

    SpriteRenderer sprite;
    float alpha = 1;

    void Awake() {
        velocity = 1 + (GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>().getNumberGamesWon()*0.1f);
    }

    private void Start() {
        target = new Vector2(0, 0);
        doDespawn = false;
        timer = 0;

        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);

        if(doDespawn) {
            timer += Time.deltaTime;
            alpha -= 0.035f;
            sprite.color = new Color(1, 1, 1, alpha);

            if(timer > 2f) {
                this.gameObject.SetActive(false);
                this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                timer = 0;
                doDespawn = false;
            }
        }
    }

    public void Despawn() {
        doDespawn = true; 
    }
}
