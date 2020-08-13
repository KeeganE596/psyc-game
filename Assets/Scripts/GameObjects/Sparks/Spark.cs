using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spark: GameObject in the Swipey game, stays still until tapped (Activated),
//then it will 'pop' and move towards the center
public class Spark : MonoBehaviour
{
    protected float velocity = 1;
    public float acceleration = 0.1f;
    protected float velocityMultiplier = 1;
    public GameObject sparkParticle;
    protected Animator anim;
    protected bool isActive = false;
    readonly Vector2 target = new Vector2(0, 0);

    void Update() {
        if (isActive) {// && LevelManager.Instance.GetIfGameIsPlaying()) {
            velocityMultiplier += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, target, ((velocity * Time.deltaTime) * velocityMultiplier));
        }
    }

    //Activate Spark when it is clicked/tapped, called from Swipey_TouchDetection
    public virtual void Activate() {  
        if(!isActive) {// && LevelManager.Instance.GetIfGameIsPlaying()) {
            Despawn();
        }
    }

    public void Deactivate() {
        isActive = false;
    }

    protected void Despawn() {
        gameObject.GetComponentInChildren<AudioSource>().Play(0);
        StartCoroutine("Pause"); //pause movement so animation can finish 
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Activate");

        GameObject ps = Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);
        ParticleSystem.MainModule pModule = ps.GetComponent<ParticleSystem>().main;
        pModule.startColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    IEnumerator Pause() {
        yield return new WaitForSeconds(0.35f);
        isActive = true;
    }
}
