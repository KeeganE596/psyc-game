using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnatSwapTouchDetection : MonoBehaviour
{
    Vector2 startPos, endPos;
    float touchTimeStart, touchTimeFinish;

    [SerializeField] 
    [Range(0.05f, 1f)] private float throwForce = 0.2f;

    Vector3 mousePos;
    RaycastHit2D hit;
    [SerializeField] private Camera mainCam;

    bool clickedGnat = false;
    GameObject clickedGnatObject;

    void Start() {
    }

    void Update() {
        //when mouse/touch clicked
        if (Input.GetMouseButtonDown(0)){// || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            //get mouse/touch click position
            startPos = Input.mousePosition;
            doClick();
        }

        //when mouse/touch released
        if (Input.GetMouseButtonUp(0)) {// || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)){
            //get release mouse/touch position
            endPos = Input.mousePosition;
            if(clickedGnat)
                doRelease();
        }
    }

    void doClick() {
        //if(LevelManager.Instance.GetIfGameIsPlaying()) {
            hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(startPos), Vector2.zero);

            //Raycast from camera
            if (hit && hit.collider.gameObject.CompareTag("Spark")) {
                //if raycast hits gameobject with tag "Spark" Run this code.
                hit.collider.GetComponent<Spark>().Activate();
                Debug.Log("here");
            }
            else if (hit && hit.collider.gameObject.CompareTag("Gnat")) {
                clickedGnat = true;
                clickedGnatObject = hit.collider.gameObject;
            }

            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;
        //}
    }

    void doRelease() {
        //if(LevelManager.Instance.GetIfGameIsPlaying()) {
            //get time when released
            touchTimeFinish = Time.time;
            Debug.Log(clickedGnatObject.name);
            if(clickedGnat) {
                //clickedGnatObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //clickedGnatObject.GetComponent<Rigidbody2D>().AddForce(-(startPos - endPos) / (touchTimeFinish - touchTimeStart) * throwForce); //(direction/timeInterval*throwforce)
                //clickedGnatObject.GetComponent<Collider2D>().enabled = false;
                //clickedGnatObject.GetComponent<Gnat>().Despawn();

                Vector2 gnatThrow = (-(startPos - endPos) / (touchTimeFinish - touchTimeStart) * throwForce);
                clickedGnatObject.GetComponent<Gnat>().Despawn(gnatThrow);
            }

            //clickedGnatObject.transform.GetChild(2).gameObject.SetActive(true);
            //clickedGnatObject.transform.GetChild(2).transform.SetParent(clickedGnatObject.transform.parent);

            clickedGnat = false;
        //}
    }
}
