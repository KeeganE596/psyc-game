using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetection : MonoBehaviour
{
    public bool enabletouch = false;

    [SerializeField]
    public Score_manager score;

    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [Range(0.05f, 1f)] //slider in inspector
    public float throwForce = 0.3f;

    GameObject gnattObject;
    string obj = "";



    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(screenPos,Vector2.zero);

        
        //Raycast from camera

        if (hit /*&& (Input.GetMouseButtonDown(0) || Input.touchCount <= 0*/)
        {

            if (hit.collider.gameObject.tag == "Spark")
            {
                //if raycast hits gameobject with tag "Spark" Run this code.
                Spark spark = hit.collider.GetComponent<Spark>();
                spark.Activate();
                obj = "spark";
            }

            if (hit.collider.gameObject.tag == "Gnatt")
            {
                obj = "gnatt";
                gnattObject = hit.collider.gameObject;
            }
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && obj == "gnatt")
        {
            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        //when finger is released
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && obj == "gnatt")
        {
            //get time when released
            touchTimeFinish = Time.time;

            // calculate swipte time interval
            timeInterval = touchTimeFinish - touchTimeStart;

            //get release finger position
            endPos = Input.GetTouch(0).position;

            //calculate swipe direction in 2D space
            direction = startPos - endPos;

            //add force onto rigid body depending on swipe time, direction and throw force.
            Rigidbody2D rb = gnattObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.gravityScale = 0;
            rb.AddForce(-direction / timeInterval * throwForce);

            CircleCollider2D gnattcollider = gnattObject.GetComponent<CircleCollider2D>();
            gnattcollider.enabled = false;

            Destroy(gnattObject, 3f);
            obj = "";
        }

    }


}
