using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gnatt: GameObject in the Swipey game, always move towards the center, 
//when swiped despawn is called and gnatt will fade away over a couple seconds
public class Gnat : MonoBehaviour
{
    protected float velocity = 1;
    protected float speedMultiplier = 0;
    //int lives = 0;

    readonly Vector2 target = new Vector2(0, 0);

    protected bool doDespawn;

    protected SpriteRenderer sprite;
    protected float alpha = 1;

    protected Animator anim;
    public GameObject destroyParticle;

    private Color angryColor = new Color32(62, 7, 7, 255);
    private Color frustratedColor = new Color32(32, 23, 47, 255);
    private Color lonelyColor = new Color32(43, 43, 43, 255);
    private Color sadColor = new Color32(5, 17, 54, 255);
    
    protected virtual void Awake() {
    }

    public virtual void Start() {
        doDespawn = false;
        sprite = this.GetComponentInChildren<SpriteRenderer>();
        anim = this.GetComponentInChildren<Animator>();
        
        //Set movement velocity to scale with level number
        //velocity = 1 + speedMultiplier;
    }

    void Update() {
        //if(LevelManager.Instance.GetIfGameIsPlaying()) {
            this.transform.position = Vector2.MoveTowards(transform.position, target, velocity * Time.deltaTime);
        //}
    }

    public virtual void Despawn() {
        this.GetComponentInChildren<AudioSource>().Play(0);
        anim.SetTrigger("destroy");
        StartCoroutine("EndGnat");
    }

    IEnumerator EndGnat() {
        yield return new WaitForSeconds(0.15f);
        SpawnParticles();
        yield return new WaitForSeconds(0.85f);
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

    public void SetSpeed(float speed) {
        velocity = 1 + speed;
    }

    private void SpawnParticles() {
        GameObject ps = Instantiate(destroyParticle, gameObject.transform.position, Quaternion.identity);
        ParticleSystem.MainModule pModule = ps.GetComponent<ParticleSystem>().main;
        if (gameObject.name == "Gnat_Angry_Red(Clone)") {
            pModule.startColor = angryColor;
        }
        else if (gameObject.name == "Gnat_Frustrated(Clone)") {
            pModule.startColor = frustratedColor;
        }
        else if (gameObject.name == "Gnat_Sad(Clone)") {
            pModule.startColor = sadColor;
        }
        else if (gameObject.name == "Gnat_Lonely(Clone)") {
            pModule.startColor = lonelyColor;
        }
    }

    public virtual void ChooseSprite() {
        return;
    }
}
