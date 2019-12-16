using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Swipey_MessageController : MonoBehaviour
{
    Animator textAnimator;
    LevelManager levelManager;
    public GameObject messageTextObj;
    TextMeshPro messageText;
    //public GameObject textBackground;

    public GameObject maybeTextObj;
    TextMeshPro maybeText;
    Animator maybeTextAnimator;
    List<Vector3> maybeTextPositions;

    bool playingChooseGame;
    bool textVisible;
    bool doubleText;

    List<string> gnattMessages;
    List<string> sparkMessages;
    List<string> bothMessages;
    List<string> gameMessages;

    float timer;

    Vector3 worldScale;
    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        playingChooseGame = levelManager.getIfPlayingChooseGame();
        textVisible = false;

        textAnimator = messageTextObj.GetComponent<Animator>();
        textAnimator.SetBool("textVisible", false);

        maybeText = maybeTextObj.GetComponent<TextMeshPro>();
        maybeTextAnimator = maybeTextObj.GetComponent<Animator>();
        maybeTextAnimator.SetBool("textVisible", false);
        maybeTextPositions = new List<Vector3>();
        maybeTextPositions.Add(new Vector3(0-worldScale.x*0.4f, 0+worldScale.y*0.4f, -4));
        maybeTextPositions.Add(new Vector3(0+worldScale.x*0.4f, 0+worldScale.y*0.4f, -4));
        maybeTextPositions.Add(new Vector3(0-worldScale.x*0.3f, 0+worldScale.y*0.6f, -4));
        maybeTextPositions.Add(new Vector3(0+worldScale.x*0.3f, 0+worldScale.y*0.6f, -4));

        doubleText = false;

        gnattMessages = new List<string>();
        gnattMessages.Add("Gnats are like gloomy thoughts"); 
        gnattMessages.Add("Swipe away the gnats to help calm the brain");
        gnattMessages.Add("GNAT stands for Gloomy Negative Automatic Thoughts");
        gnattMessages.Add("They can come fast and automatically, so you don’t even notice and the day gets gloomy");
        gnattMessages.Add("Flick them away to reveal the calm");

        sparkMessages = new List<string>();
        sparkMessages.Add("SPARX are like positive thoughts"); 
        sparkMessages.Add("Tap the SPARX to calm the brain");
        sparkMessages.Add("In facts SPARX stands for Smart Relastic X factor thoughts");
        sparkMessages.Add("Focusing on the SPARX helps grow them");

        bothMessages = new List<string>();
        bothMessages.Add("Find the SPARX amongst the GNATs"); 
        bothMessages.Add("Focusing your attention on the SPARX helps grow them");

        gameMessages = new List<string>();
        gameMessages.Add("Phew");   gameMessages.Add("Isn't it nice to relax"); gameMessages.Add("Chill Out");
        gameMessages.Add("Take a break sometimes");  gameMessages.Add("Uplift your brain");   gameMessages.Add("Do just one thing");
        gameMessages.Add("Sometimes it's hard to see the SPARX"); //Sometimes it's hard to see the SPARX amongst the GNATs
        gameMessages.Add("Grow the SPARX!");

        timer = 0;
        messageText = messageTextObj.GetComponent<TextMeshPro>();

        scaleText();
    }

    // Update is called once per frame
    void Update() {
        if(levelManager.getIfPlayingChooseGame() && levelManager.isPlaying()) {
            
            timer += Time.deltaTime;
            //gnatt messages
            if(levelManager.getGamesWon() == 0) {
                doubleText = true;
                if(timer < 6.5) {
                    messageText.text = gnattMessages[0];
                }
                else {
                    messageText.text = gnattMessages[1];
                }
            }
            else if(levelManager.getGamesWon() == 1) {
                messageText.text = gnattMessages[2];
            }
            else if(levelManager.getGamesWon() == 2) {
                messageText.text = gnattMessages[3];
            }
            else if(levelManager.getGamesWon() == 3) {
                messageText.text = gnattMessages[4];
            }
            //spark messages
            else if(levelManager.getGamesWon() == 4) {
                doubleText = true;
                if(timer < 6.5) {
                    messageText.text = sparkMessages[0];
                }
                else {
                    messageText.text = sparkMessages[1];
                }
            }
            else if(levelManager.getGamesWon() == 5) {
                messageText.text = sparkMessages[2];
            }
            else if(levelManager.getGamesWon() == 6) {
                messageText.text = sparkMessages[3];
            }
            //messages for both spawning
            else if(levelManager.getGamesWon() == 8) {
                messageText.text = bothMessages[0];
            }
            else if(levelManager.getGamesWon() == 9) {
                messageText.text = bothMessages[1];
            }
            else {
                timer = 15;
            }

            //Debug.Log(messageText.preferredWidth + ", " + (worldScale.x*2)*0.85f);
            if(messageText.preferredWidth >= (worldScale.x*2)*0.8f) {
                DoubleBackgroundSize(true);
            }
            else {
                DoubleBackgroundSize(false);
            }

            if(timer <= 5) {
                textVisible = true;
            }
            else if(doubleText && timer >= 6.5 && timer <= 13){
                textVisible = true;
            }
            else {
                textVisible = false;
            }

            //Debug.Log(timer);
            textAnimator.SetBool("textVisible", textVisible);
        }
        else {
            textVisible = false;
        }
    }

    void scaleText() {
        messageText.fontSize = SCREEN_HEIGHT/190f;
        messageTextObj.GetComponent<RectTransform>().localPosition = new Vector2(0, (0+worldScale.y)-(worldScale.y*2)*0.85f);

        DoubleBackgroundSize(false);

        maybeText.fontSize = SCREEN_HEIGHT/275f;
    }

    void DoubleBackgroundSize(bool isDouble) {
        GameObject textBackground = messageTextObj.transform.GetChild(0).gameObject;
        if(isDouble == true) {
            textBackground.transform.localScale = new Vector2((worldScale.x)*0.9f, (worldScale.y)*0.18f);
        }
        else {
            textBackground.transform.localScale = new Vector2((worldScale.x)*0.9f, (worldScale.y)*0.09f);
        }
    }

    public void MaybeSaySomething() {
        int rand = Random.Range(0, 100);

        if(rand <= 30 && !maybeTextAnimator.GetBool("textVisible")) {
            maybeText.transform.position = maybeTextPositions[Random.Range(0, maybeTextPositions.Count)];
            maybeText.text = gameMessages[Random.Range(0, gameMessages.Count)];
            StartCoroutine("showMessage");
        }
    }

    IEnumerator showMessage() {
        maybeTextAnimator.SetBool("textVisible", true);
        yield return new WaitForSeconds(4);
        maybeTextAnimator.SetBool("textVisible", false);
    }
}
