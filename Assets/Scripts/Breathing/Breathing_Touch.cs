﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Breathing_Touch: touch detection for breathing game. Also manages the score
public class Breathing_Touch : MonoBehaviour
{
    public GameObject expandBlob;
    Transform expandBlobTransform;
    public GameObject breatheLimitParent;
    float outerAreaMax;
    float outerAreaMin;
    float innerAreaMax;
    float innerAreaMin;
    float outerAreaMidPoint;
    float innerAreaMidPoint;

    Animator circleColorAnimator;

    Vector3 worldScale;
    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    public GameObject countCircle;
    Color offColor = new Color32(150, 150, 150, 255);
    Color onColor = Color.white;
    int breatheCount;
    int maxPoints;
    List<GameObject> scoreCounters;

    float expandSpeed;
    float compressSpeed;

    public GameObject breatheTextObj;
    TextMeshPro breatheText; 

    bool held;
    bool inBreathe;
    float breatheTimer;
    LevelManager levelManager;

    int isBreathingIn;
    bool wasLastBreathingIn;
    float holdTimer;
    bool isHolding = false;

    int levelScaler;

    public float breatheInTime = 4;
    public float breatheOutTime = 6;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        levelScaler = GameManagerStatic.GetCurrentLevelNumber();

        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        expandBlobTransform = expandBlob.GetComponent<Transform>();
        breatheText = breatheTextObj.GetComponent<TextMeshPro>();

        circleColorAnimator = breatheLimitParent.GetComponent<Animator>();
        circleColorAnimator.SetBool("outerGreen", true);
        //circleColorAnimator.SetBool("innerGreen", false);

        outerAreaMax = breatheLimitParent.transform.GetChild(1).gameObject.transform.localScale.x;
        outerAreaMin = breatheLimitParent.transform.GetChild(2).gameObject.transform.localScale.x;
        innerAreaMax = breatheLimitParent.transform.GetChild(3).gameObject.transform.localScale.x;
        innerAreaMin = breatheLimitParent.transform.GetChild(4).gameObject.transform.localScale.x;
        outerAreaMidPoint = (outerAreaMax + outerAreaMin)/2;
        innerAreaMidPoint = (innerAreaMax + innerAreaMin)/2;

        isBreathingIn = 1;
        holdTimer = 0;

        breatheCount = -1;
        
        if(levelScaler < 3) { maxPoints = 3; }
        else if(levelScaler < 6) { maxPoints = 4; }
        else { maxPoints = 5; }

        expandSpeed = ((outerAreaMidPoint - expandBlobTransform.localScale.x)/breatheInTime)/(1.0f / Time.deltaTime);

        setupScoreCounters();
    }

    // Update is called once per frame
    void Update() {
        if(levelManager.isPlaying()) {
            if(Input.GetMouseButtonDown(0) && !checkClickInMenuButton()) {
                if(checkInGrey()) {
                    setNotHolding();
                }
                else if(checkInInnerGreen() && breatheTimer > 3.5f) {
                    setNotHolding();
                }
            }
            else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
                if(checkInOuterGreen() && !isHolding) {
                    setHolding(4);
                    wasLastBreathingIn = true;
                }
                else if(checkInInnerGreen() && !isHolding) {
                    setHolding(3);
                    wasLastBreathingIn = false;
                }
            }
            else if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) ) {
                if(holdTimer > 1 && isHolding && !checkClickInMenuButton()) {
                    setNotHolding();
                }
                if(wasLastBreathingIn) {
                    breatheTimer = breatheOutTime+1;
                    compressSpeed = ((expandBlobTransform.localScale.x - innerAreaMidPoint)/breatheOutTime)/(1.0f / Time.deltaTime); //calculate compressSpeed based on distance and bretahe out time
                    circleColorAnimator.SetBool("outerGreen", false);
                }
                else {
                    breatheTimer = breatheInTime+1;
                    expandSpeed = ((outerAreaMidPoint - expandBlobTransform.localScale.x)/breatheInTime)/(1.0f / Time.deltaTime); //calculate expandSpeed based on distance and breathe in time
                    if(isHolding) {
                        breatheCount++;
                        setScoreCounters();
                    }
                    circleColorAnimator.SetBool("outerGreen", true);
                }
            }
            else {
                if(holdTimer <= 1) {
                    isHolding = false;
                    if(wasLastBreathingIn) { isBreathingIn = -1; }
                    else { isBreathingIn = 1; }
                }

                if(isBreathingIn==1 && !isHolding) {
                    
                    expandBlobTransform.localScale += new Vector3(expandSpeed, expandSpeed, 0);
                    breatheTimer -= Time.deltaTime;
                }
                else if(isBreathingIn==-1 && !isHolding) {
                    
                    expandBlobTransform.localScale -= new Vector3(compressSpeed, compressSpeed, 0);
                    breatheTimer -= Time.deltaTime;
                }
            }

            if(isHolding && isBreathingIn==0 && holdTimer >= 1) {
                holdTimer -= Time.deltaTime;
            }

            setBreatheText();
            //Debug.Log(breatheCount);
            if(breatheCount == maxPoints-1 && levelManager.isPlaying()) {
                levelManager.GameWon();
            }
        }
    }

    public void setBreatheText() {
        if(isBreathingIn==1) {
            breatheText.text = "Breathe In.." + (int) breatheTimer + "..";
        }
        else if(isBreathingIn==-1) {
            breatheText.text = "Breathe Out.." + (int) breatheTimer + "..";
        }
        else if(holdTimer <= 1 && !wasLastBreathingIn) {
            breatheText.text = "Breathe In..-..";
        }
        else if(holdTimer <= 1 && wasLastBreathingIn) {
            breatheText.text = "Breathe Out..-..";
        }
        else {
            breatheText.text = "Hold.." + (int) holdTimer + "..";
        }
    }

    void setupScoreCounters() {
        scoreCounters = new List<GameObject>();
        for(int i=0; i<maxPoints; i++) {
            //Vector3 pos = new Vector3(0-2*(worldScale.x/3), (0+worldScale.y)-((worldScale.y*2)/(maxPoints+1))*(i+1), 0);
            Vector3 pos = new Vector3(0-2*(worldScale.x/3), (0+(worldScale.y/2))-((worldScale.y)/(maxPoints-1))*(i), 0);
            scoreCounters.Add(Instantiate(countCircle, pos, Quaternion.identity));
            //scoreCounters[i].GetComponent<SpriteRenderer>().color = offColor;
        }
    }

    void setScoreCounters() {
        for(int i=0; i<maxPoints; i++) {
            if(i <= breatheCount) {
                //scoreCounters[i].GetComponent<SpriteRenderer>().color = onColor;
                scoreCounters[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else {
                //scoreCounters[i].GetComponent<SpriteRenderer>().color = offColor;
                scoreCounters[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    void setHolding(int holdTime) {
        isBreathingIn = 0;
        isHolding = true;
        holdTimer = holdTime;
    }

    void setNotHolding() {  
        if(breatheCount > -1) { breatheCount -= 1; }
        setScoreCounters();
        expandBlob.transform.localScale = new Vector3(innerAreaMin, innerAreaMin, 0);
        isBreathingIn = 1;
        isHolding = false;
        holdTimer = 0;
        wasLastBreathingIn = false;
    }

    bool checkClickInMenuButton() {
        float mXPos = -(SCREEN_WIDTH*0.5f)+(SCREEN_WIDTH*0.06f);
        float mYPos = (SCREEN_HEIGHT*0.5f)-(SCREEN_HEIGHT*0.9f);
        float size = SCREEN_HEIGHT*0.08f;
        if(Input.mousePosition.x >= mXPos-(size/2) && Input.mousePosition.x <= mXPos+(size/2) &&
                Input.mousePosition.y <= mYPos-(size/2) && Input.mousePosition.y >= mXPos+(size/2)){
            return true;
        }
        else {
            return false;
        }
    }

    bool checkInInnerGreen() {
        if(expandBlobTransform.localScale.x >= innerAreaMin && expandBlobTransform.localScale.x <= innerAreaMax) {
            return true;
        }
        else {
            return false;
        }
    }
    bool checkInOuterGreen() {
        if(expandBlobTransform.localScale.x >= outerAreaMin && expandBlobTransform.localScale.x <= outerAreaMax) {
            return true;
        }
        else {
            return false;
        }
    }

    bool checkInGrey() {
        if((expandBlobTransform.localScale.x > innerAreaMax && expandBlobTransform.localScale.x < outerAreaMin) ||
                expandBlobTransform.localScale.x > outerAreaMax+0.1f) {
            return true;
        }
        else {
            return false;
        }
    }
    
}
