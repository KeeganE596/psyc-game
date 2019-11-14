using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Swipey_ScoreManager: manages the game score and center blob animations,
//ends the game when score is reached
public class Swipey_ScoreManager : MonoBehaviour
{
    LevelManager levelManager;
    int score = 0;
    Text scoreText;
    private Animator anim;
    private SpriteRenderer m_SpriteRenderer;
    private bool ishurt = false;
    public bool vulnerable = true;
    public GameObject Rings;
    Object_Spawner spawner;

    //public Spark Spark;

     CameraShake cameraShake;

    [HideInInspector]
    public Camera effectCamera;

    bool levelEnded;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Start() {
        score = 0; 
        Rings.SetActive(false);
        anim = gameObject.GetComponent<Animator>();
        m_SpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        
        levelEnded = false;
    }

    public void addScore() {
        score = score + 10;
    }
    public void minusScore() {
        //StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
        //cameraShake.transform.position = new Vector3 (0,0,0);
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
        gameObject.GetComponent<Collider2D>().enabled = false;

        //makes colliders invincible for animation
        vulnerable = false;

        levelManager.GameWon();
    }

    // Update is called once per frame
    void Update() {
        //scoreText.text = score.ToString();
        if (score < 50) {
            anim.SetBool("50 Points", false);
            anim.SetBool("100 Points", false);
            anim.SetBool("150 Points", false);
        }
        else if (score >= 50 && score < 100) {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", false);
            anim.SetBool("150 Points", false);
        }
        else if (score >= 100 && score < 150) {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", true);
            anim.SetBool("150 Points", false);
        }
        else if (score >= 150 && score < 200) {
            anim.SetBool("50 Points", true);
            anim.SetBool("100 Points", true);
            anim.SetBool("150 Points", true);
        }
        
        if (score >= 90 && !levelEnded) {
            levelEnded = true;
            endLevel();
            //scoreText.text = "Level Complete";
        }

        if (ishurt) {
            StartCoroutine("flashHurt");
            ishurt = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Spark" && vulnerable == true && levelManager.isPlaying()) {          
            addScore();
            this.gameObject.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1f, 1f, 0);
            col.gameObject.GetComponent<Spark>().Deactivate();
            col.gameObject.SetActive(false);
        }
        if(col.gameObject.tag == "Gnatt" && vulnerable == true && levelManager.isPlaying()) {
            minusScore();
            ishurt = true;
            col.gameObject.SetActive(false);
        }
    }



}
