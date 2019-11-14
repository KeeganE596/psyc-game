using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Breathing_TouchDetection: REDUNDANT - replaced with Breathing_Touch. Keeping in case of switch back.
//Old touch mechanics, hold down to grow circle, release when in large green ring, wait for circle
//to contract, when in small green ring tap and hold again to grow ring, repeat.
public class Breathing_TouchDetection : MonoBehaviour
{
    public GameObject expandBlob;
    public GameObject breatheLimitParent;
    float outerAreaMax;
    float outerAreaMin;    
    float innerAreaMax;
    float innerAreaMin;

    public GameObject countCircle_1;
    public GameObject countCircle_2;
    public GameObject countCircle_3;
    Color offColor = new Color32(150, 150, 150, 255);
    Color onColor = Color.white;

    public float expandSpeed = 0.014f;
    public float compressSpeed = 0.01f;

    public GameObject breatheTextObj;
    TextMeshPro breatheText; 

    bool held;
    bool inBreathe;
    int breatheCount;
    int maxPoints;
    int breatheTimer;
    public GameObject canvas;
    LevelManager levelManagerScript;

    void Awake() {
        levelManagerScript = canvas.GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        held = false;
        breatheCount = 0;
        breatheTimer = 0;

        //setCountMarkers();
        
        //maxPoints = levelManagerScript.maxPoints;
        maxPoints = 1000;

        outerAreaMax = breatheLimitParent.transform.GetChild(0).gameObject.transform.localScale.x;
        outerAreaMin = breatheLimitParent.transform.GetChild(1).gameObject.transform.localScale.x;
        innerAreaMax = breatheLimitParent.transform.GetChild(2).gameObject.transform.localScale.x;
        innerAreaMin = breatheLimitParent.transform.GetChild(3).gameObject.transform.localScale.x;

        breatheText = breatheTextObj.GetComponent<TextMeshPro>();
    }

    

    // Update is called once per frame
    void Update() {
        //If mouse/touch is held expand 'breathe meter'
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            expandBlob.transform.localScale += new Vector3(expandSpeed, expandSpeed, 0);  //breathing expand speed
            held = true;

            breatheTimer += (int) Time.deltaTime;
            setBreatheText();
        }
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            if(expandBlob.transform.localScale.x < outerAreaMin || expandBlob.transform.localScale.x > outerAreaMax) {
                held = false;
                inBreathe = false;
                breatheCount = 0;
                setCountMarkers();
            }
            else {
                inBreathe = true;
            }
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            if(expandBlob.transform.localScale.x <= innerAreaMax && expandBlob.transform.localScale.x >= innerAreaMin && inBreathe) {
                breatheCount++;
                setCountMarkers();
            }
            else {
                inBreathe = false;
                breatheCount = 0;
                setCountMarkers();
            }
        }

        if (expandBlob.transform.localScale.x > 0.4 && !held) {
            expandBlob.transform.localScale = new Vector3(0.4f, 0.4f, 0);
        }
        //Do breathe out decrease meter
        else if(held && !Input.GetMouseButton(0)) {
            expandBlob.transform.localScale -= new Vector3(compressSpeed, compressSpeed, 0); //breathe ring compression speed
        }

        if(breatheCount >= maxPoints) {
            expandBlob.transform.localScale += new Vector3(expandSpeed, expandSpeed, 0);
            breatheCount = 0;
            levelManagerScript.GameWon();
        }
    }

    void setCountMarkers() {
        if(breatheCount == 0) {
            countCircle_1.gameObject.GetComponent<SpriteRenderer>().color = offColor;
            countCircle_2.gameObject.GetComponent<SpriteRenderer>().color = offColor;
            countCircle_3.gameObject.GetComponent<SpriteRenderer>().color = offColor;
        }
        else if(breatheCount == 1) {
            countCircle_1.gameObject.GetComponent<SpriteRenderer>().color = onColor;
        }
        else if(breatheCount == 2) {
            countCircle_2.gameObject.GetComponent<SpriteRenderer>().color = onColor;
        }
        else if(breatheCount == 3) {
            countCircle_3.gameObject.GetComponent<SpriteRenderer>().color = onColor;
        }
    }

    public void setBreatheText() {
        if(!inBreathe) {
            breatheText.text = "Breathe In.." + breatheTimer + "..";
        }
        else {
            breatheTimer = 0;
            breatheText.text = "Breathe Out.." + breatheTimer + "..";
        }
    }
}