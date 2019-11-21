using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inspection_TouchDetection : MonoBehaviour
{
    public GameObject thoughtTextObj;
    TextMeshPro thoughtTextArea;
    Vector3 screenPos;
    RaycastHit2D hit;
    GameObject currentThought;
    public GameObject thoughtsControllerObj;
    Inspection_ThoughtsController thoughtsController;

    void Awake() {
        thoughtTextArea = thoughtTextObj.GetComponent<TextMeshPro>();
        thoughtsController = thoughtsControllerObj.GetComponent<Inspection_ThoughtsController>();
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(screenPos,Vector2.zero);

            if(hit && hit.collider.gameObject.CompareTag("Thought")) {
                thoughtsController.clickedThought();

                currentThought = hit.collider.gameObject;
                thoughtTextArea.text = currentThought.GetComponent<Inspection_ThoughtObject>().getThought();
            }
            else {
                thoughtsController.clickedThought(false);
                thoughtTextArea.text = "...";
                currentThought = null;
            }
        }
    }

    public void ClickGoodButton() {
        if(currentThought != null && currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnGreen();
        }
        else if(currentThought != null) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnRed();
        }
        thoughtsController.clickedThought();
    }

    public void ClickBadButon() {
        if(currentThought != null && !currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            thoughtsController.RemoveThought(currentThought);
        }
        else if(currentThought != null) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnRed();
        }
        thoughtsController.clickedThought();
    }
}
