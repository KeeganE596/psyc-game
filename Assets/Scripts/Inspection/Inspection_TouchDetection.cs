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

    void Awake() {
        thoughtTextArea = thoughtTextObj.GetComponent<TextMeshPro>();
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
                currentThought = hit.collider.gameObject;

                thoughtTextArea.text = currentThought.GetComponent<Inspection_ThoughtObject>().getThought();
            }
            else {
                thoughtTextArea.text = "...";
                currentThought = null;
            }
        }
    }

    public void ClickGoodButton() {
        if(currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            Debug.Log("Yay");
        }
    }

    public void ClickBadButon() {
        if(!currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            Debug.Log("Yas");
        }
    }
}
