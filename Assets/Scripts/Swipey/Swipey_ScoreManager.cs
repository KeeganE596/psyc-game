using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Swipey_ScoreManager: manages the game score and center blob animations,
//ends the game when score is reached
public class Swipey_ScoreManager : MonoBehaviour
{
    LevelManager levelManager;
    int score = 5;
    Text scoreText;
    private bool ishurt = false;
    public bool vulnerable = true;

    int maxPoints = 15;
    Vector3 worldScale;

    bool levelEnded;

    GameObject backgroundIndicator;
    GameObject scoreIndicator;

    float scoreScaleAmount;
    float scoreIndicatorTargetSize;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        score = 5;
        
        levelEnded = false;

        scoreIndicator = gameObject.transform.GetChild(1).gameObject;
        backgroundIndicator = gameObject.transform.GetChild(0).gameObject;
        
        scoreScaleAmount = (backgroundIndicator.transform.localScale.x - scoreIndicator.transform.localScale.x)/maxPoints;
    }

    public void addScore() {
        score += 1;
        scoreIndicatorTargetSize = scoreIndicator.transform.localScale.x + scoreScaleAmount;
        while(scoreIndicator.transform.localScale.x < scoreIndicatorTargetSize) {
            scoreIndicator.transform.localScale += new Vector3(0.005f, 0.005f, 0);
        }
    }
    public void minusScore() {
        score -= 1;
        scoreIndicator.transform.localScale -= new Vector3(scoreScaleAmount, scoreScaleAmount, 0);
    }

    public void EndLevel(bool wonGame) {
        //disables score collider
        gameObject.GetComponent<Collider2D>().enabled = false;
        //makes colliders invincible for animation
        vulnerable = false;

        if(wonGame) {
            levelManager.GameWon();
        }
        else {
            levelManager.GameLost();
        }
        
    }

    // Update is called once per frame
    void Update() {
        if(score <= 0) {
            levelEnded = true;
            EndLevel(false);
        }
        if (score >= maxPoints && !levelEnded) {
            levelEnded = true;
            EndLevel(true);
        }

        if (ishurt) {
            ishurt = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Spark" && vulnerable == true && levelManager.isPlaying()) {          
            addScore();
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
