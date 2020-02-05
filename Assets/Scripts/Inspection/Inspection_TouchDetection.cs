using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inspection_TouchDetection : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject canvas;
    public GameObject thoughtTextObj;
    TextMeshPro thoughtTextArea;
    Vector3 screenPos;
    RaycastHit2D hit;
    GameObject currentThought;
    public GameObject thoughtsControllerObj;
    Inspection_ThoughtsController thoughtsController;

    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        thoughtTextArea = thoughtTextObj.GetComponent<TextMeshPro>();
        thoughtsController = thoughtsControllerObj.GetComponent<Inspection_ThoughtsController>();
    }
    // Start is called before the first frame update
    void Start() {
        canvas.transform.GetChild(8).gameObject.SetActive(false);
        canvas.transform.GetChild(9).gameObject.SetActive(false);
        thoughtTextObj.SetActive(false);

        ScaleButtons();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(screenPos,Vector2.zero);

            if(hit && hit.collider.gameObject.CompareTag("Thought")) {
                thoughtsController.clickedThought(true);

                if(currentThought != null) { currentThought.GetComponent<Inspection_ThoughtObject>().HideFocusRing(); }
                currentThought = hit.collider.gameObject;
                thoughtTextArea.text = currentThought.GetComponent<Inspection_ThoughtObject>().getThought();
                currentThought.GetComponent<Inspection_ThoughtObject>().ShowFocusRing();
            }
            else {
                thoughtsController.clickedThought(false);
                thoughtTextArea.text = "...";
                if(currentThought != null) { currentThought.GetComponent<Inspection_ThoughtObject>().HideFocusRing(); }
                currentThought = null;
            }
        }

        if(levelManager.isPlaying()) {
            canvas.transform.GetChild(8).gameObject.SetActive(true);
            canvas.transform.GetChild(9).gameObject.SetActive(true);
            thoughtTextObj.SetActive(true);
        }
        else {
            canvas.transform.GetChild(8).gameObject.SetActive(false);
            canvas.transform.GetChild(9).gameObject.SetActive(false);
            thoughtTextObj.SetActive(false);
        }
    }

    public void ClickGoodButton() {
        currentThought.GetComponent<Inspection_ThoughtObject>().HideFocusRing();
        if(currentThought != null && currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnToSpark();
        }
        else if(currentThought != null) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnRed();
        }
        thoughtsController.clickedThought();
        CheckWin();

    }

    public void ClickBadButon() {
        currentThought.GetComponent<Inspection_ThoughtObject>().HideFocusRing();
        if(currentThought != null && !currentThought.GetComponent<Inspection_ThoughtObject>().getIfPositive()) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnToGnatt();
            //thoughtsController.RemoveThought(currentThought);
        }
        else if(currentThought != null) {
            currentThought.GetComponent<Inspection_ThoughtObject>().turnRed();
        }
        thoughtsController.clickedThought();
        CheckWin();
    }

    public void ScaleButtons() {
        GameObject badButton = canvas.transform.GetChild(8).gameObject;
        badButton.GetComponent<RectTransform>().localPosition = new Vector2(0-SCREEN_WIDTH*0.33f, (0-SCREEN_HEIGHT*0.25f));

        GameObject goodButton = canvas.transform.GetChild(9).gameObject;
        goodButton.GetComponent<RectTransform>().localPosition = new Vector2(0+SCREEN_WIDTH*0.33f, (0-SCREEN_HEIGHT*0.25f));

        Vector3 worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        thoughtTextObj.transform.position = new Vector2(0, (0-worldScale.y*0.50f));
        thoughtTextObj.GetComponent<TextMeshPro>().fontSize = 23;
    }

    public void CheckWin() {
        if(thoughtsController.CheckIfWin()) {
            levelManager.GameWon();
        }
    }
}
