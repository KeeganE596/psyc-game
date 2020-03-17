using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds_TouchDetection : MonoBehaviour
{
    LevelManager levelManager;

    Vector3 mousePos;
    RaycastHit2D hit;
    Vector2 startPos, endPos;

    public GameObject cloudsParentObj;
    List<GameObject> clouds;

    int score = 0;
    bool levelEnded = false;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void Start() {
        clouds = new List<GameObject>();
        foreach (Transform cloudChild in cloudsParentObj.transform) {   //get child clouds from parent obj
            clouds.Add(cloudChild.gameObject);
        }
        RandomizeClouds();
    }

    // Update is called once per frame
    void Update() {
        //when mouse/touch clicked
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            //get mouse/touch click position
            startPos = Input.mousePosition;
            doClick();
        }

        if(score >= 12 && !levelEnded) {
            levelEnded = true;
            levelManager.GameWon();
        }
    }

    void doClick() {
        if(levelManager.isPlaying()) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(startPos), Vector2.zero);

            //Raycast from camera
            if (hit && hit.collider.gameObject.CompareTag("Spark")) {
                //if raycast hits gameobject with tag "Spark" Run this code.
                hit.collider.GetComponent<Spark>().Activate();
                score++;
                ReduceClouds();
                //messageController.MaybeSaySomething();
            }
            // else if (hit && hit.collider.gameObject.CompareTag("Gnatt")) {
            //     clickedGnat = true;
            //     clickedGnatObject = hit.collider.gameObject;
            // }

            // //get touch position and mark time when screen is touched
            // touchTimeStart = Time.time;
        }
    }

    void ReduceClouds() {
        for(int i=0; i<4; i++) {
            int cloudIndex = Random.Range(0, clouds.Count);
            SpriteRenderer c = clouds[cloudIndex].GetComponent<SpriteRenderer>();
            Color currentColor = c.color;
            c.color = new Color(currentColor.r + ((1-currentColor.r)/2), 
                                currentColor.g + ((1-currentColor.g)/2), 
                                currentColor.b + ((1-currentColor.b)/2), 
                                currentColor.a - (currentColor.a*0.15f));
        }
    }

    void RandomizeClouds() {
        foreach (GameObject cloud in clouds) {
            SpriteRenderer c = cloud.GetComponent<SpriteRenderer>();
            float tint = Random.Range(0.3f, 0.6f);
            c.color = new Color(tint, tint, tint, Random.Range(0.85f, 0.95f)); 
        }
    }
}
