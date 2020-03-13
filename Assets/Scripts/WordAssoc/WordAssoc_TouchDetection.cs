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
    
    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void Start() {
        // Add a Line Renderer to the GameObject
        //  line = this.gameObject.AddComponent<LineRenderer>();
        //  line.startWidth = (0.04f);
        //  line.endWidth = (0.05f);
        //  line.SetPosition(0, new Vector2(0, 0));
        //  line.startColor = new Color(1, 1, 1, 1);
        //  line.endColor = new Color(1, 1, 1, 1);
        //  line.material = lineMat;
         //line.gameObject.layer = 11;    //add to metaball layer
    }
    
    // Update is called once per frame
    void Update() {
        if(levelManager.isPlaying()) {
            mousePos = Input.mousePosition;
            screenPos = Camera.main.ScreenToWorldPoint(mousePos);
            hit = Physics2D.Raycast(screenPos,Vector2.zero);

            //Raycast from camera
            // if (hit && hit.collider.gameObject.tag == "TouchBlob")  {
            //     hitObj = true;
            // }
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
                //touch_blob.SetActive(true);
                //touch_blob.transform.position = toPos;
                //line.SetPosition(1, toPos);
            }
            // else{
                // Vector2 newPos = 
                //         Vector2.MoveTowards(new Vector2(touch_blob.transform.position.x, touch_blob.transform.position.y), new Vector2(0,0), (25 * Time.deltaTime));
                // touch_blob.transform.position = newPos;
                // line.SetPosition(1, newPos);
                // touch_blob.SetActive(false);
            // }

            if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
                hitObj = false;
                if(hit && hit.collider.CompareTag("Player")) {
                    currentLine.SetPosition(1, new Vector2(0,0));
                    currentWord.GetComponent<Collider2D>().enabled = false;
                }
                else if(currentWord) {
                    currentLine.enabled = false;
                }
            }
        }
    }
}
