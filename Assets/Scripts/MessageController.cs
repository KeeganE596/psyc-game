using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    protected Animator textAnimator;
    protected LevelManager levelManager;
    public GameObject messageTextObj;
    protected TextMeshProUGUI messageText;

    protected Vector3 worldScale;
    protected float SCREEN_WIDTH = Screen.width;
    protected float SCREEN_HEIGHT = Screen.height;

    protected bool hasStarted = false;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    // Start is called before the first frame update
    protected virtual void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(SCREEN_WIDTH, SCREEN_HEIGHT, 0f));
        textAnimator = messageTextObj.GetComponent<Animator>();
        messageText = messageTextObj.GetComponentInChildren<TextMeshProUGUI>();

        scaleText();

        messageTextObj.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
    }

    void scaleText() {
        //messageText.fontSize = SCREEN_HEIGHT/190f;
        //messageTextObj.GetComponent<RectTransform>().localPosition = new Vector2(0, (0+SCREEN_HEIGHT)-(SCREEN_HEIGHT)*0.85f);

        //messageText.fontSize = SCREEN_HEIGHT/190f;
        messageTextObj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0-(SCREEN_HEIGHT*0.35f));

        //DoubleBackgroundSize(false);
        RectTransform textBackground = messageTextObj.transform.GetChild(0).GetComponent<RectTransform>();
        textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.15f);
    }

    void DoubleBackgroundSize(bool isDouble) {
        RectTransform textBackground = messageTextObj.transform.GetChild(0).GetComponent<RectTransform>();
        if(isDouble == true) {
            textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.18f);
        }
        else {
            textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.16f);
        }
    }
}
