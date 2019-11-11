using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Breathing_Touch : MonoBehaviour
{
    public GameObject expandBlob;
    Transform expandBlobTransform;
    public GameObject outerAreaMaxObj;
    float outerAreaMax;
    public GameObject outerAreaMinObj;
    float outerAreaMin;
    public GameObject innerAreaMaxObj;
    float innerAreaMax;
    public GameObject innerAreaMinObj;
    float innerAreaMin;

    public GameObject countCircle_1;
    public GameObject countCircle_2;
    public GameObject countCircle_3;
    Color offColor = new Color32(150, 150, 150, 255);
    Color onColor = Color.white;

    public float expandSpeed = 0.003f;
    public float compressSpeed = 0.002f;

    public GameObject breatheTextObj;
    TextMeshPro breatheText; 

    bool held;
    bool inBreathe;
    int breatheCount;
    int maxPoints;
    float breatheTimer;
    public GameObject canvas;
    LevelManager levelManagerScript;

    int isBreathingIn;
    bool wasLastBreathingIn;
    float holdTimer;
    bool isHolding = false;

    // Start is called before the first frame update
    void Start()
    {
        expandBlobTransform = expandBlob.GetComponent<Transform>();
        breatheText = breatheTextObj.GetComponent<TextMeshPro>();

        outerAreaMax = outerAreaMaxObj.transform.localScale.x;
        outerAreaMin = outerAreaMinObj.transform.localScale.x;
        innerAreaMax = innerAreaMaxObj.transform.localScale.x;
        innerAreaMin = innerAreaMinObj.transform.localScale.x;

        isBreathingIn = 1;
        holdTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            if(expandBlobTransform.localScale.x >= outerAreaMin && expandBlobTransform.localScale.x <= outerAreaMax && !isHolding) {
                isBreathingIn = 0;
                isHolding = true;
                holdTimer = 4;
                wasLastBreathingIn = true;
            }
            else if(expandBlobTransform.localScale.x <= innerAreaMax && expandBlobTransform.localScale.x >= innerAreaMin && !isHolding) {
                isBreathingIn = 0;
                isHolding = true;
                holdTimer = 3;
                wasLastBreathingIn = false;
            }
            else if(!isHolding) {
                expandBlob.transform.localScale = new Vector3(innerAreaMin, innerAreaMin, 0);
                isBreathingIn = 1;
                isHolding = false;
                holdTimer = 0;
                wasLastBreathingIn = false;
            }
        }
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            if(wasLastBreathingIn) {
                breatheTimer = 6;
            }
            else {
                breatheTimer = 4;
            }
        }
        else {
            if(expandBlobTransform.localScale.x >= outerAreaMax + 0.2f) {
                expandBlob.transform.localScale = new Vector3(innerAreaMin, innerAreaMin, 0);
                isBreathingIn = 1;
                isHolding = false;
                holdTimer = 0;
                wasLastBreathingIn = false;
            }

            if(isBreathingIn==1 && !isHolding) {
                expandBlobTransform.localScale += new Vector3(expandSpeed, expandSpeed, 0);
                breatheTimer -= Time.deltaTime;
                Debug.Log("here");
            }
            else if(isBreathingIn==-1 && !isHolding) {
                expandBlobTransform.localScale -= new Vector3(compressSpeed, compressSpeed, 0);
                breatheTimer -= Time.deltaTime;
                Debug.Log("there");
            }

            if(holdTimer <= 1) {
                isHolding = false;
                if(wasLastBreathingIn) {
                    isBreathingIn = -1;
                }
                else {
                    isBreathingIn = 1;
                }
            }
        }

        if(isHolding && isBreathingIn==0 && holdTimer >= 1) {
            holdTimer -= Time.deltaTime;
        }
        

        setBreatheText();
        Debug.Log("breathing in: " + breatheTimer);
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
}
