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
    
    protected virtual void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public virtual void Start() {
        doDespawn = false;
        timer = 0;
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        
        //Set movement velocity to scale with level number
        velocity = 1 + speedMultiplier;
    }

    void Update() {
        if(levelManager.isPlaying()) {
            this.transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);
        }
    }

    public virtual void Despawn() {
        //this.GetComponentInChildren<AudioSource>().Play(0);
        StartCoroutine("EndGnat");
    }

    IEnumerator EndGnat() {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        doDespawn = false;
    }

    public virtual int GetLives() {
        return 0;
    }

    public virtual void FlipSprite() {
        this.transform.localScale = new Vector3(-(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
    }
}
