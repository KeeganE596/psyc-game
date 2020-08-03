﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Swipey_TouchDetection: takes touch and mouse input and activates appropriate methods
//if touch was on a spark or swipe occured
public class Swipey_TouchDetection : MonoBehaviour
{
    LevelManager levelManager;
    
    Vector2 startPos, endPos;
    float touchTimeStart, touchTimeFinish;

    [SerializeField]
    [Range(0.05f, 1f)] //slider in inspector
    private float throwForce = 0.2f;

    public GameObject aoePrefab;
    GameObject aoeObject;
    Swipey_AoeSwipe aoeScript;

    Vector3 mousePos;
    RaycastHit2D hit;

    //public Swipey_MessageController messageController;

    bool clickedGnat;
    GameObject clickedGnatObject;
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Start() {
        aoeObject = Instantiate(aoePrefab, new Vector2(0, 0), Quaternion.identity);
        aoeScript = aoeObject.GetComponent<Swipey_AoeSwipe>();
        aoeObject.SetActive(false);

        clickedGnat = false;
    }

    private void Update() {
        //when mouse/touch clicked
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            //get mouse/touch click position
            startPos = Input.mousePosition;
            doClick();
        }

        //when mouse/touch released
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)){
            //get release mouse/touch position
            endPos = Input.mousePosition;
            doRelease();
        }
    }

    void doClick() {
        if(levelManager.isPlaying()) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(startPos), Vector2.zero);

            //Raycast from camera
            if (hit && hit.collider.gameObject.CompareTag("Spark")) {
                //if raycast hits gameobject with tag "Spark" Run this code.
                hit.collider.GetComponent<Spark>().Activate();
                //messageController.MaybeSaySomething();
            }
            else if (hit && hit.collider.gameObject.CompareTag("Gnatt")) {
                clickedGnat = true;
                clickedGnatObject = hit.collider.gameObject;
            }

            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;

            //Move aoeObject for detecting gnatts in swipe
            //aoeObject.SetActive(true);
            //aoeScript.clearGnatts();
            //aoeObject.transform.position = Camera.main.ScreenToWorldPoint(startPos);
        }
    }

    void doRelease() {
        if(levelManager.isPlaying()) {
            //get time when released
            touchTimeFinish = Time.time;

            if(clickedGnat) {
                clickedGnatObject.GetComponent<Rigidbody2D>().AddForce(-(startPos - endPos) / (touchTimeFinish - touchTimeStart) * throwForce); //(direction/timeInterval*throwforce)
                clickedGnatObject.GetComponent<Collider2D>().enabled = false;
                clickedGnatObject.GetComponent<Gnatt>().Despawn();
            }

            clickedGnat = false;

            // //Flick gnatts away
            // if(startPos != endPos && !aoeScript.isGnattsEmpty()) {
            //     aoeScript.flickGnatts((startPos - endPos), (touchTimeFinish - touchTimeStart), throwForce);
            //     //messageController.MaybeSaySomething();
            // }
            // aoeObject.SetActive(false);
        }
    }
}
