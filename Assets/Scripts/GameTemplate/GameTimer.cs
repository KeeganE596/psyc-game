using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    public float timeRemaining;
    float maxTime = 8f;

    public Slider timeSlider;
    bool startTimer;

    GameObject gameManager;
    GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        //timeSlider.SetActive(false);
        startTimer = false;
        timeRemaining = maxTime;
        gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if(startTimer) {
            timeSlider.value = CalculateSliderValue();

            if(timeRemaining <= 0) {
                timeRemaining = 0;
                //gameManager.GetComponent<GameManager>.NextGame();
                gameManagerScript.NextGame();
            }
            if(timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
        }
    }

    float CalculateSliderValue() {
        return timeRemaining/ maxTime;
    }

    public void startTimerNow(){
       // timeSlider.SetActive(true);
        startTimer = true;
    }
}
