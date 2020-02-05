using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gnatt: GameObject in the Swipey game, always move towards the center, 
//when swiped despawn is called and gnatt will fade away over a couple seconds
public class Gnatt : MonoBehaviour
{
    protected float velocity = 1;
    protected float speedMultiplier = 0;
    //int lives = 0;

    readonly Vector2 target = new Vector2(0, 0);

    protected bool doDespawn;
    float timer;

    protected SpriteRenderer sprite;
    protected float alpha = 1;

    LevelManager levelManager;
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public virtual void Start() {
        doDespawn = false;
        timer = 0;
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        //Set movement velocity to scale with level number
        velocity = 1 + speedMultiplier;
    }

    void Update() {
        if(levelManager.isPlaying()) {
            transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);
        }
        if(doDespawn) {
            //Debug.Log("here");
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

    public virtual void Despawn() {
        doDespawn = true;
        sprite.color = new Color(1, 1, 1, alpha);
    }

    public virtual int GetLives() {
        return 0;
    }
}
