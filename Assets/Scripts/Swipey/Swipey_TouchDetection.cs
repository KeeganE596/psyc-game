using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipey_TouchDetection : MonoBehaviour
{
    public bool enabletouch = false;

    [SerializeField]
    public Swipey_ScoreManager score;

    Vector2 startPos, endPos;
    float touchTimeStart, touchTimeFinish;

    [Range(0.05f, 1f)] //slider in inspector
    public float throwForce = 0.3f;

    GameObject gnattObject;
    public GameObject aoePrefab;
    GameObject aoeObject;
    Swipey_AoeSwipe aoeScript;
    

    Vector3 mousePos;
    RaycastHit2D hit;

    private void Start() {
        aoeObject = Instantiate(aoePrefab, new Vector2(0, 0), Quaternion.identity);
        aoeScript = aoeObject.GetComponent<Swipey_AoeSwipe>();
        aoeObject.SetActive(false);
    }

    private void Update()
    {
        //when mouse/touch clicked
        if (Input.GetMouseButtonDown(0)) {
            //get mouse click position
            startPos = Input.mousePosition;
            doClick();
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            //get finger tap position
            startPos = Input.GetTouch(0).position;
            doClick();
        }

        //when mouse/touch released
        if (Input.GetMouseButtonUp(0)){
            //get release mouse position
            endPos = Input.mousePosition;
            doRelease();
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            //get release finger position
            endPos = Input.GetTouch(0).position;
            doRelease();
        }
    }

    void doClick() {
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

    void doRelease() {
        //get time when released
        touchTimeFinish = Time.time;

        //Flick gnatts away
        if(startPos != endPos && !aoeScript.isGnattsEmpty()) {
            aoeScript.flickGnatts((startPos - endPos), (touchTimeFinish - touchTimeStart), throwForce);
        }
        aoeObject.SetActive(false);
    }
}
