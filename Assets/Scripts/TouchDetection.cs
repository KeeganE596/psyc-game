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

    GameObject aoeObject;
    public GameObject aoePrefab;
    

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
            }

           // if (hit.collider.gameObject.tag == "Gnatt")
            //{
            //    gnattObject = hit.collider.gameObject;
            //}
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //get touch position and mark time when screen is touched
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;

            //Make AOE object to detect for gnatt collisions with swipe
            Vector2 aoeStartPos = Camera.main.ScreenToWorldPoint(startPos);
            aoeObject = Instantiate(aoePrefab, aoeStartPos, Quaternion.identity);
        }

        //when finger is released
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //get time when released
            touchTimeFinish = Time.time;
            // calculate swipe time interval
            timeInterval = touchTimeFinish - touchTimeStart;
            //get release finger position
            endPos = Input.GetTouch(0).position;
            //calculate swipe direction in 2D space
            direction = startPos - endPos;

            //Move AOE swipe with actual touch input swipe
            Vector2 aoeStartPos = Camera.main.ScreenToWorldPoint(startPos);
            Vector2 aoeEndPos = Camera.main.ScreenToWorldPoint(endPos);
            aoeObject.transform.position = Vector2.MoveTowards(aoeStartPos, aoeEndPos, (1 * Time.deltaTime));

            if(startPos != endPos && !aoeObject.GetComponent<AoeSwipe>().isGnattsEmpty()) {
                List<GameObject> hitGnatts = aoeObject.GetComponent<AoeSwipe>().getGnatts();
                foreach(GameObject g in hitGnatts) {
                    //add force onto rigid body depending on swipe time, direction and throw force.
                    Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                    rb.AddForce(-direction / timeInterval * throwForce);

                    g.GetComponent<CircleCollider2D>().enabled = false;

                    Destroy(g, 2f);
                }
            }
            
            Destroy(aoeObject, 0.5f);
        }
    }
}
