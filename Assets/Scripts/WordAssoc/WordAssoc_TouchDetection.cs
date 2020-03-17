using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WordAssoc_TouchDetection:Detect mouse/touch input and move the touch blob to follow the input
//if held down
public class WordAssoc_TouchDetection : MonoBehaviour
{
    //public GameObject touch_blob;
    //public Material lineMat;

    bool hitObj;

    //LineRenderer line;

    Vector3 mousePos;
    Vector3 screenPos;
    RaycastHit2D hit;

    LevelManager levelManager;

    GameObject currentWord;
    LineRenderer currentLine;

    int score;
    public GameObject scoreObject;
    GameObject backgroundIndicator;
    GameObject scoreIndicator;
    float scoreScaleAmount;
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void Start() {
        score = 0;

        backgroundIndicator = scoreObject.transform.GetChild(0).gameObject;
        scoreIndicator = scoreObject.transform.GetChild(1).gameObject;
        scoreScaleAmount = (backgroundIndicator.transform.localScale.x - scoreIndicator.transform.localScale.x)/(8);
    }
    
    // Update is called once per frame
    void Update() {
        if(levelManager.isPlaying()) {
            mousePos = Input.mousePosition;
            screenPos = Camera.main.ScreenToWorldPoint(mousePos);
            hit = Physics2D.Raycast(screenPos,Vector2.zero);

            if (hit && hit.collider.CompareTag("Word") && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))  {
                hitObj = true;
                currentWord = hit.collider.gameObject;
                currentLine = currentWord.GetComponentInChildren<LineRenderer>();
                currentLine.enabled = true;
                currentLine.SetPosition(0, currentLine.transform.position);
            }

            if(hitObj && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
                Vector2 toPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.SetPosition(1, toPos);
            }

            if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
                hitObj = false;
                if(hit && hit.collider.CompareTag("Player")) {
                    currentLine.SetPosition(1, new Vector2(0,0));
                    currentWord.GetComponent<Collider2D>().enabled = false;
                    
                    score++;
                    scoreIndicator.transform.localScale += new Vector3(scoreScaleAmount, scoreScaleAmount, 0);
                }
                currentWord = null;
                currentLine = null;
            }

            if(score >= 8) {
                levelManager.GameWon();
            }
        }
        Debug.Log(score);
    }

    void UpdateScoreCircle() {
        //scoreIndicatorTargetSize = scoreIndicator.transform.localScale.x + scoreScaleAmount;
        //while(scoreIndicator.transform.localScale.x < scoreIndicatorTargetSize) {
            scoreIndicator.transform.localScale += new Vector3(scoreScaleAmount, scoreScaleAmount, 0);
        //}
    }
}
