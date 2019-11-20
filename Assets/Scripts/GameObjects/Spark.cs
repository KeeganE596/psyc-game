using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spark: GameObject in the Swipey game, stays still until tapped (Activated),
//then it will 'pop' and move towards the center
public class Spark : MonoBehaviour
{
    float velocity = 1;
    public float acceleration = 0.1f;
    float velocityMultiplier;
    public GameObject sparkParticle;
    Animator anim;
    bool isActive;
    readonly Vector2 target = new Vector2(0, 0);

    LevelManager levelManager;
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    private void Start() {
        isActive = false;
        velocityMultiplier = 1;

        //Debug.Log(gameObject.name);

        //plays correct spawn animation based off instantiated name
        if (gameObject.name == "Spark_01(Clone)")
        {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetTrigger("SparkPrefab_01");
        }
        if (gameObject.name == "Spark_02(Clone)") {
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetTrigger("SparkPrefab_02");
        } 
    }

    private void Update() {
        if (isActive && levelManager.isPlaying()) {
            velocityMultiplier += acceleration;
            transform.position = Vector2.MoveTowards(transform.position, target, ((velocity * Time.deltaTime) * velocityMultiplier));
        }
    }

    //Activate Spark when it is clicked/tapped, called from Swipey_TouchDetection
    public void Activate() {  
        if(!isActive && levelManager.isPlaying()) {
            gameObject.GetComponentInChildren<AudioSource>().Play(0);
            StartCoroutine("Pause"); //pause movement so animation can finish 
            anim = this.gameObject.GetComponent<Animator>();
            anim.SetTrigger("Activate");
            Instantiate(sparkParticle, gameObject.transform.position, Quaternion.identity);
        }
    }

    public void Deactivate() {    
        isActive = false;
    }

    IEnumerator Pause() {
        yield return new WaitForSeconds(0.35f);
        isActive = true;
    }
}
