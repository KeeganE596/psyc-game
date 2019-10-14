using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordAssoc_TouchDetection : MonoBehaviour
{
    public GameObject touch_blob;

    bool hitObj;

    LineRenderer line;

    Vector3 mousePos;
    Vector3 screenPos;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        // Add a Line Renderer to the GameObject
         line = this.gameObject.AddComponent<LineRenderer>();
         line.startWidth = (0.04f);
         line.endWidth = (0.05f);
         line.SetPosition(0, new Vector2(0, 0));
         line.gameObject.layer = 11;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        screenPos = Camera.main.ScreenToWorldPoint(mousePos);
        hit = Physics2D.Raycast(screenPos,Vector2.zero);

        //Raycast from camera
        if (hit && hit.collider.gameObject.tag == "TouchBlob")  {
            hitObj = true;
        }

        if(hitObj && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
            Vector2 toPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touch_blob.transform.position = toPos;
            line.SetPosition(1, toPos);
        }
        else{
            Vector2 newPos = Vector2.MoveTowards(
                new Vector2(touch_blob.transform.position.x, touch_blob.transform.position.y), new Vector2(0,0), (25 * Time.deltaTime));
            touch_blob.transform.position = newPos;
            line.SetPosition(1, newPos);
        }

        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            hitObj = false;
        }

    }
}
