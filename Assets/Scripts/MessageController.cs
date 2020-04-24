using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageController : MonoBehaviour
{
    protected Animator textAnimator;
    protected LevelManager levelManager;
    //public GameObject messageTextObj;
    protected TextMeshProUGUI messageText;
    public GameObject monkMessageObject;
    protected TextMeshPro monkMessageText;

    protected Vector3 worldScale;
    protected float SCREEN_WIDTH = Screen.width;
    protected float SCREEN_HEIGHT = Screen.height;

    protected bool hasStarted = false;

    protected int currentGamesWon;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    // Start is called before the first frame update
    protected virtual void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(SCREEN_WIDTH, SCREEN_HEIGHT, 0f));
        //textAnimator = messageTextObj.GetComponent<Animator>();
        //messageText = messageTextObj.GetComponentInChildren<TextMeshProUGUI>();

        currentGamesWon = levelManager.getGamesWon();

        scaleText();

        //messageTextObj.SetActive(false);
        
        monkMessageText = monkMessageObject.GetComponentInChildren<TextMeshPro>();
        monkMessageObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
    }

    void scaleText() {
        //messageText.fontSize = SCREEN_HEIGHT/190f;
        //messageTextObj.GetComponent<RectTransform>().localPosition = new Vector2(0, (0+SCREEN_HEIGHT)-(SCREEN_HEIGHT)*0.85f);

        //messageText.fontSize = SCREEN_HEIGHT/190f;
        //messageTextObj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0-(SCREEN_HEIGHT*0.35f));

        //DoubleBackgroundSize(false);
        //RectTransform textBackground = messageTextObj.transform.GetChild(0).GetComponent<RectTransform>();
        //textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.15f);
    }

    void DoubleBackgroundSize(bool isDouble) {
        // RectTransform textBackground = messageTextObj.transform.GetChild(0).GetComponent<RectTransform>();
        // if(isDouble == true) {
        //     textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.18f);
        // }
        // else {
        //     textBackground.sizeDelta = new Vector2((SCREEN_WIDTH)*0.9f, (SCREEN_HEIGHT)*0.16f);
        // }
    }

    public void ContinueGame() {
        levelManager.StartPlaying();
        monkMessageObject.SetActive(false);
    }

    protected IEnumerator CycleGameTextSingle(string gameText) {
        // messageTextObj.SetActive(true);
        // messageText.text = gameText;
        // textAnimator.SetTrigger("showText");
        monkMessageText.text = gameText;
        yield return new WaitForSeconds(6f);

        //textAnimator.SetTrigger("fadeOut");
        //yield return new WaitForSeconds(1f);
        //messageTextObj.SetActive(false);
    }

    protected IEnumerator CycleGameTextDouble(string gameText_1, string gameText_2) {
        // messageTextObj.SetActive(true);
        // messageText.text = gameText_1;
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = gameText_2;
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);
        
        // textAnimator.SetTrigger("fadeOut");
        // yield return new WaitForSeconds(1f);
        // messageTextObj.SetActive(false);
        GameObject button = monkMessageObject.GetComponentInChildren<Button>().gameObject;
        GameObject playText = monkMessageObject.transform.GetChild(3).gameObject;
        button.SetActive(false);
        playText.SetActive(false);
        monkMessageText.text = gameText_1;
        yield return new WaitForSeconds(4.5f);

        monkMessageText.text = gameText_2;
        button.SetActive(true);
        playText.SetActive(true);
        yield return new WaitForSeconds(1f);

        //textAnimator.SetTrigger("fadeOut");
        //yield return new WaitForSeconds(1f);
    }

    protected IEnumerator CycleGameText(List<string> textList) {
        GameObject button = monkMessageObject.GetComponentInChildren<Button>().gameObject;
        GameObject playText = monkMessageObject.transform.GetChild(3).gameObject;
        button.SetActive(false);
        playText.SetActive(false);

        foreach(string text in textList) {
            monkMessageText.text = text;
            yield return new WaitForSeconds(3.5f);
        }
        
        ContinueGame();
        //button.SetActive(true);
        //playText.SetActive(true);
    }
}
