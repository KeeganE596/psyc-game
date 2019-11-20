using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gnatt: GameObject in the Swipey game, always move towards the center, 
//when swiped despawn is called and gnatt will fade away over a couple seconds
public class Gnatt : MonoBehaviour
{
    float velocity = 1;

    readonly Vector2 target = new Vector2(0, 0);

    bool doDespawn;
    float timer;

    SpriteRenderer sprite;
    float alpha = 1;

    LevelManager levelManager;
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Start() {
        doDespawn = false;
        timer = 0;
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //Set movement velocity to scale with level number
        velocity = 1 + levelManager.getNumberGamesWon()*0.1f;
    }

    private void Update() {
        if(levelManager.isPlaying()) {
            transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);

            if(doDespawn) {
                timer += Time.deltaTime;
                //Fade out sprite
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
    }

    public void Despawn() {
        doDespawn = true; 
    }
}
