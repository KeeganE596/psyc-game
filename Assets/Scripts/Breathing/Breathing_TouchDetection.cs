using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_TouchDetection : MonoBehaviour
{
    public GameObject expandBlob;

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

    public float expandSpeed = 0.014f;
    public float compressSpeed = 0.01f;

    bool held;
    bool inBreathe;
    int breatheCount;
    int maxPoints;

    public GameObject canvas;
    LevelManager levelManagerScript;

    // Start is called before the first frame update
    void Start() {
        held = false;
        breatheCount = 0;

        setCountMarkers();

        levelManagerScript = canvas.GetComponent<LevelManager>();
        maxPoints = levelManagerScript.maxPoints;

        outerAreaMax = outerAreaMaxObj.transform.localScale.x;
        outerAreaMin = outerAreaMinObj.transform.localScale.x;
        innerAreaMax = innerAreaMaxObj.transform.localScale.x;
        innerAreaMin = innerAreaMinObj.transform.localScale.x;
    }

    

    // Update is called once per frame
    void Update() {
        //If mouse/touch is held expand 'breathe meter'
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            expandBlob.transform.localScale += new Vector3(expandSpeed, expandSpeed, 0);  //breathing expand speed
            held = true;
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
}