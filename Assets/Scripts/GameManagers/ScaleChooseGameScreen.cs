using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleChooseGameScreen : MonoBehaviour
{
    public GameObject canvas;
    private float SCREEN_WIDTH = Screen.width;
    private float SCREEN_HEIGHT = Screen.height;


    // Start is called before the first frame update
    void Start() {
        scaleScreen();
    }

    public void scaleScreen() {
        GameObject titleText = canvas.transform.GetChild(0).gameObject;
        titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT*0.1f);
        titleText.GetComponent<RectTransform>().localPosition = new Vector2(0, (SCREEN_HEIGHT*0.5f)-SCREEN_HEIGHT*0.25f);
        titleText.GetComponent<TextMeshProUGUI>().fontSize = SCREEN_HEIGHT/15f;

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ChooseButton");
        float count = 1;

        foreach(GameObject b in buttons) {
            b.GetComponent<RectTransform>().sizeDelta = new Vector2(SCREEN_WIDTH*0.2f, SCREEN_WIDTH*0.2f);
            if(count == 1) {
                b.GetComponent<RectTransform>().localPosition = new Vector2(-(SCREEN_WIDTH*0.25f), SCREEN_HEIGHT*0);
            }
            else if(count == 2) {
                b.GetComponent<RectTransform>().localPosition = new Vector2(0, SCREEN_HEIGHT*0);
            }
            else if(count == 3) {
                b.GetComponent<RectTransform>().localPosition = new Vector2(SCREEN_WIDTH*0.25f, SCREEN_HEIGHT*0);
            }
            count++;
        }
    }
}
