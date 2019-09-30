using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipey_ScoreManager : MonoBehaviour
{
    public GameObject levelManagerObj;
    LevelManager levelManager;
    public int score = 0;
    public Text scoreText;
    private Animator anim;
    private SpriteRenderer m_SpriteRenderer;
    private bool ishurt = false;
    public bool vulnerable = true;
    public GameObject Rings;
    public Object_Spawner spawner;

    public Spark Spark;

    public CameraShake cameraShake;

    [HideInInspector]
    public Camera effectCamera;

    bool levelEnded;

  
    private void Start()
    {
        score = 0; 
        Rings.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
        m_SpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        levelManager = levelManagerObj.GetComponent<LevelManager>();
        levelEnded = false;
    }

    public void addScore() {
        score = score + 10;
    }
    public void minusScore() {

        StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
        cameraShake.transform.position = new Vector3 (0,0,0);
        score = score - 10;

        if (score >= 0 && score < 50) {
            anim.SetTrigger("Hurt_1");
            anim.ResetTrigger("Hurt_2");
            anim.ResetTrigger("Hurt_3");
        }
        if (score >= 50 && score < 100) {
            anim.ResetTrigger("Hurt_1");
            anim.SetTrigger("Hurt_2");
            anim.ResetTrigger("Hurt_3");
        }
        if (score >= 100 && score < 150)
        {
            anim.ResetTrigger("Hurt_1");
            anim.ResetTrigger("Hurt_2");
            anim.SetTrigger("Hurt_3");
        }

    }
    IEnumerator flashHurt() {
        m_SpriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.enabled = true;
    }

    public void resetAnimations() {
        anim.ResetTrigger("Hurt_1");
        anim.ResetTrigger("Hurt_2");
        anim.ResetTrigger("Hurt_3");
        anim.SetBool("50 Points", false);
        anim.SetBool("100 Points", false);
        anim.SetBool("150 Points", false);
        anim.ResetTrigger("End");
    }

    public void endLevel() {

        //disables score collider and sets up gameobjects for animation
        Rings.SetActive(true);
        anim.SetTrigger("End");

        //makes colliders invincible for animation
        vulnerable = false;

        //stop spawning sparks and gnatts
        spawner.spawningGnatt = false;
        spawner.spawningSpark = false;

        //changes colliders of Gnatts to dynamic to allow pushing collision on final animation
        //changeColliders();

        levelManager.GameWon();
    }


    // public void changeColliders() {
    //     foreach(GameObject g in GameObject.FindGameObjectsWithTag("Gnatt"))
    //     {
    //         Rigidbody2D body = g.GetComponent<Rigidbody2D>();
    //         body.bodyType = RigidbodyType2D.Dynamic;
    //     }
        
    // }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = score.ToString();
        if (score < 50) {
            anim.SetBool("50 Points", false);
            anim.SetBool("100 Points", false);
            anim.SetBool("150 Points", false);
        }

        if (score >= 50 && score < 100) {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", false);
            anim.SetBool("150 Points", false);
        }
        if (score >= 100 && score < 150)
        {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", true);
            anim.SetBool("150 Points", false);
        }
        if (score >= 150 && score < 200)
        {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", true);
            anim.SetBool("150 Points", true);
        }
        if (score >= 80 && !levelEnded) {
            levelEnded = true;
            endLevel();
            //scoreText.text = "Level Complete";
        }

        if (ishurt == true) {
            StartCoroutine(flashHurt());
            ishurt = false;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spark" && vulnerable == true) {           
            addScore();
            this.gameObject.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1f, 1f, 0);
            col.gameObject.GetComponent<Spark>().Deactivate();
            col.gameObject.SetActive(false);
        }
        if(col.gameObject.tag == "Gnatt" && vulnerable == true) {
            minusScore();
            ishurt = true;
            col.gameObject.SetActive(false);
        }
    }



}
