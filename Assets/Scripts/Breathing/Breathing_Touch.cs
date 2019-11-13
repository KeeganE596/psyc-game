using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Breathing_Touch : MonoBehaviour
{
    public GameObject expandBlob;
    Transform expandBlobTransform;
    public GameObject breatheLimitParent;
    float outerAreaMax;
    float outerAreaMin;
    float innerAreaMax;
    float innerAreaMin;

    Vector3 worldScale;

    public GameObject countCircle;
    Color offColor = new Color32(150, 150, 150, 255);
    Color onColor = Color.white;
    int breatheCount;
    int maxPoints;
    List<GameObject> scoreCounters;

    public float expandSpeed = 0.003f;
    public float compressSpeed = 0.002f;

    public GameObject breatheTextObj;
    TextMeshPro breatheText; 

    bool held;
    bool inBreathe;
    float breatheTimer;
    public GameObject canvas;
    LevelManager levelManagerScript;

    int isBreathingIn;
    bool wasLastBreathingIn;
    float holdTimer;
    bool isHolding = false;

    int levelScaler;

    void Awake() {
        levelManagerScript = canvas.GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        levelScaler = levelManagerScript.getNumberGamesWon();

        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        expandBlobTransform = expandBlob.GetComponent<Transform>();
        breatheText = breatheTextObj.GetComponent<TextMeshPro>();

        outerAreaMax = breatheLimitParent.transform.GetChild(0).gameObject.transform.localScale.x;
        outerAreaMin = breatheLimitParent.transform.GetChild(1).gameObject.transform.localScale.x;
        innerAreaMax = breatheLimitParent.transform.GetChild(2).gameObject.transform.localScale.x;
        innerAreaMin = breatheLimitParent.transform.GetChild(3).gameObject.transform.localScale.x;

        isBreathingIn = 1;
        holdTimer = 0;

        breatheCount = -1;

        
        if(levelScaler%2 == 0 && levelScaler-3 > 0) {
            maxPoints = 3 + (levelScaler-3);
        }
        else if(levelScaler == 0 || levelScaler == 1){ 
            maxPoints = 3;
        }
        else {
            maxPoints = 3 + (levelScaler-2);
        }

        setupScoreCounters();
    }

    // Update is called once per frame
    void Update() {
        if(levelManagerScript.isPlaying()) {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
                if(expandBlobTransform.localScale.x >= outerAreaMin && expandBlobTransform.localScale.x <= outerAreaMax && !isHolding) {
                    setHolding(4);
                    wasLastBreathingIn = true;
                }
                else if(expandBlobTransform.localScale.x > innerAreaMin && expandBlobTransform.localScale.x <= innerAreaMax && !isHolding) {
                    setHolding(3);
                    wasLastBreathingIn = false;
                }
                else if(!isHolding) {
                    setNotHolding();
                }
            }
            else if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
                if(wasLastBreathingIn) {
                    breatheTimer = 6;
                }
                else {
                    breatheTimer = 4;
                    if(isHolding) {
                        breatheCount++;
                        setScoreCounters();
                    }
                }
            }
            else {
                if(holdTimer <= 1) {
                    isHolding = false;
                    if(wasLastBreathingIn) { isBreathingIn = -1; }
                    else { isBreathingIn = 1; }
                }

                if(expandBlobTransform.localScale.x >= outerAreaMax + 0.2f || 
                        (expandBlobTransform.localScale.x > outerAreaMin && expandBlobTransform.localScale.x <= outerAreaMax && isHolding) ||
                        (expandBlobTransform.localScale.x > innerAreaMin && expandBlobTransform.localScale.x <= innerAreaMax && isHolding)) {
                    setNotHolding();
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
            //Debug.Log(isHolding);
            if(breatheCount == maxPoints-1) {
                levelManagerScript.GameWon();
            }
        }
    }

    public void setBreatheText() {
        if(isBreathingIn==1) {
            breatheText.text = "Breathe In.." + (int) breatheTimer + "..";
        }
        else if(isBreathingIn==-1) {
            //breatheTimer = 0;
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
            Vector3 pos = new Vector3(0-2*(worldScale.x/3), (0+worldScale.y)-((worldScale.y/(maxPoints+1))*(i+1))-(worldScale.y/maxPoints), 0);
            scoreCounters.Add(Instantiate(countCircle, pos, Quaternion.identity));
            scoreCounters[i].GetComponent<SpriteRenderer>().color = offColor;
        }
    }

    void setScoreCounters() {
        scoreCounters[breatheCount].GetComponent<SpriteRenderer>().color = onColor;
    }

    void setHolding(int holdTime) {
        isBreathingIn = 0;
        isHolding = true;
        holdTimer = holdTime;
    }

    void setNotHolding() {        
        setupScoreCounters();
        expandBlob.transform.localScale = new Vector3(innerAreaMin, innerAreaMin, 0);
        isBreathingIn = 1;
        isHolding = false;
        holdTimer = 0;
        wasLastBreathingIn = false;
        breatheCount = -1;
    }
}
