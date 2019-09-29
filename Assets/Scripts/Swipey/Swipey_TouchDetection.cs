using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipey_TouchDetection : MonoBehaviour
{
    public bool enabletouch = false;

    [SerializeField]
    public Score_manager score;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [Range(0.05f, 1f)] //slider in inspector
    public float throwForce = 0.3f;

    GameObject gnattObject;
    public GameObject aoePrefab;
    GameObject aoeObject;
    AoeSwipe aoeScript;
    

    Vector3 mousePos;
    RaycastHit2D hit;

    private void Start() {
        aoeObject = Instantiate(aoePrefab, new Vector2(0, 0), Quaternion.identity);
        aoeScript = aoeObject.GetComponent<AoeSwipe>();
        aoeObject.SetActive(false);
    }

    private void Update()
    {
        //when mouse/touch clicked
        if (Input.GetMouseButtonDown(0)) {
            startPos = Input.mousePosition;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(startPos), Vector2.zero);

            //Raycast from camera
            if (hit && hit.collider.gameObject.CompareTag("Spark")) {
                //if raycast hits gameobject with tag "Spark" Run this code.
                hit.collider.GetComponent<Spark>().Activate();
            }

            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;

            //Move aoeObject for detecting gnatts in swipe
            aoeObject.SetActive(true);
            aoeScript.clearGnatts();
            aoeObject.transform.position = Camera.main.ScreenToWorldPoint(startPos);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startPos = Input.GetTouch(0).position;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(startPos), Vector2.zero);

            //Raycast from camera
            if (hit && hit.collider.gameObject.CompareTag("Spark")) {
                //if raycast hits gameobject with tag "Spark" Run this code.
                hit.collider.GetComponent<Spark>().Activate();
            }

            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;

            //Move aoeObject for detecting gnatts in swipe
            aoeObject.SetActive(true);
            aoeScript.clearGnatts();
            aoeObject.transform.position = Camera.main.ScreenToWorldPoint(startPos);
        }

        //when mouse/touch released
        if (Input.GetMouseButtonUp(0)){
            //get time when released
            touchTimeFinish = Time.time;
            // calculate swipe time interval
            timeInterval = touchTimeFinish - touchTimeStart;
            //get release finger position
            endPos = Input.mousePosition;
            //calculate swipe direction in 2D space
            direction = startPos - endPos;

            //Flick gnatts away
            if(startPos != endPos && !aoeScript.isGnattsEmpty()) {
                aoeScript.flickGnatts(direction, timeInterval, throwForce);
                aoeScript.clearGnatts();
            }
            aoeObject.SetActive(false);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            //get time when released
            touchTimeFinish = Time.time;
            // calculate swipe time interval
            timeInterval = touchTimeFinish - touchTimeStart;
            //get release finger position
            endPos = Input.GetTouch(0).position;
            //calculate swipe direction in 2D space
            direction = startPos - endPos;

            //Flick gnatts away
            if(startPos != endPos && !aoeScript.isGnattsEmpty()) {
                aoeScript.flickGnatts(direction, timeInterval, throwForce);
                aoeScript.clearGnatts();
            }
            aoeObject.SetActive(false);
        }
    }
}
