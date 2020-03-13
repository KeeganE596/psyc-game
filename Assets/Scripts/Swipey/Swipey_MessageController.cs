using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Swipey_MessageController : MessageController
{
    public GameObject maybeTextObj;
    TextMeshPro maybeText;
    Animator maybeTextAnimator;
    List<Vector3> maybeTextPositions;

    List<string> monkMessages;
    // List<string> sparkMessages;
    // List<string> bothMessages;
    List<string> gameMessages;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();

        //fill arrays for game bar text instructions
        monkMessages = new List<string>();
        // gnattMessages.Add("Gnats are like gloomy thoughts"); 
        // gnattMessages.Add("Swipe away the gnats to help calm the brain");
        // gnattMessages.Add("GNAT stands for Gloomy Negative Automatic Thoughts");
        // gnattMessages.Add("They can come fast and automatically, so you don’t even notice and the day gets gloomy");
        // gnattMessages.Add("Flick them away to reveal the calm");
        monkMessages.Add("<b>GNAT</b> stands for <b>Gloomy Negative Automatic Thoughts</b>. Swipe them away to relax the Zen Ninja"); 
        monkMessages.Add("<b>SPARX</b> stand for <b>Smart Positive Automatic Realistic X Factor</b> thoughts. Tap to collect them and relax the Zen Ninja");
        monkMessages.Add("<b>SPARX</b> and <b>GNATs</b> can come at the same time, try to find the <b>SPARX</b> amongst the <b>GNATs</b>");
        monkMessages.Add("<b>GNATs</b> can come fast, automatic and in all shapes and forms. Try find their new form and swipe them away");
        monkMessages.Add("<b>SPARX</b> can also come fast, automatic and in all shapes and forms. Try find their new form and tap them");

        // sparkMessages = new List<string>();
        // sparkMessages.Add("SPARX are like positive thoughts"); 
        // sparkMessages.Add("Tap the SPARX to calm the brain");
        // sparkMessages.Add("In facts SPARX stands for Smart Relastic X factor thoughts");
        // sparkMessages.Add("Focusing on the SPARX helps grow them");

        // bothMessages = new List<string>();
        // bothMessages.Add("Find the SPARX amongst the GNATs"); 
        // bothMessages.Add("Focusing your attention on the SPARX helps grow them");

        //get positions and fill array for maybe text
        maybeText = maybeTextObj.GetComponent<TextMeshPro>();
        maybeTextAnimator = maybeTextObj.GetComponent<Animator>();
        maybeTextAnimator.SetBool("textVisible", false);
        maybeTextPositions = new List<Vector3>();
        maybeTextPositions.Add(new Vector3(0-worldScale.x*0.4f, 0+worldScale.y*0.4f, -4));
        maybeTextPositions.Add(new Vector3(0+worldScale.x*0.4f, 0+worldScale.y*0.4f, -4));
        maybeTextPositions.Add(new Vector3(0-worldScale.x*0.3f, 0+worldScale.y*0.6f, -4));
        maybeTextPositions.Add(new Vector3(0+worldScale.x*0.3f, 0+worldScale.y*0.6f, -4));

        gameMessages = new List<string>();
        gameMessages.Add("Phew");   gameMessages.Add("Isn't it nice to relax"); gameMessages.Add("Chill Out");
        gameMessages.Add("Take a break sometimes");  gameMessages.Add("Uplift your brain");   gameMessages.Add("Do just one thing");
        gameMessages.Add("Sometimes it's hard to see the SPARX"); //Sometimes it's hard to see the SPARX amongst the GNATs
        gameMessages.Add("Grow the SPARX!");    gameMessages.Add("Nice job!");

        maybeText.fontSize = SCREEN_HEIGHT/275f;
    }

    // Update is called once per frame
    void Update() {
        // if(levelManager.getIfPlayingChooseGame() && levelManager.isPlaying() && !hasStarted) {
        //     hasStarted = true;

        //     //gnatt messages
        //     if(levelManager.getGamesWon() == 0) {
        //         StartCoroutine(CycleGameTextDouble(gnattMessages[0], gnattMessages[1]));
        //     }
        //     else if(levelManager.getGamesWon() == 1) {
        //         StartCoroutine(CycleGameTextSingle(gnattMessages[2]));
        //     }
        //     else if(levelManager.getGamesWon() == 2) {
        //         StartCoroutine(CycleGameTextSingle(gnattMessages[3]));
        //     }
        //     else if(levelManager.getGamesWon() == 3) {
        //         StartCoroutine(CycleGameTextSingle(gnattMessages[4]));
        //     }
        //     //spark messages
        //     else if(levelManager.getGamesWon() == 4) {
        //         StartCoroutine(CycleGameTextDouble(sparkMessages[0], sparkMessages[1]));
        //     }
        //     else if(levelManager.getGamesWon() == 5) {
        //         StartCoroutine(CycleGameTextSingle(sparkMessages[2]));
        //     }
        //     else if(levelManager.getGamesWon() == 6) {
        //         StartCoroutine(CycleGameTextSingle(sparkMessages[3]));
        //     }
        //     //messages for both spawning
        //     else if(levelManager.getGamesWon() == 8) {
        //         StartCoroutine(CycleGameTextSingle(bothMessages[0]));
        //     }
        //     else if(levelManager.getGamesWon() == 9) {
        //         StartCoroutine(CycleGameTextSingle(bothMessages[1]));
        //     }
        // }
        if(!levelManager.getIfPlayingChooseGame() && hasStarted) {
            if(currentGamesWon == 0) {
                StartCoroutine(CycleGameTextSingle(monkMessages[0]));
            }
            else if(currentGamesWon == 3) {
                StartCoroutine(CycleGameTextSingle(monkMessages[1]));
            }
            else if(currentGamesWon == 6) {
                StartCoroutine(CycleGameTextSingle(monkMessages[2]));
            }
            else if(currentGamesWon == 11) {
                StartCoroutine(CycleGameTextSingle(monkMessages[3]));
            }
            else if(currentGamesWon == 14) {
                StartCoroutine(CycleGameTextSingle(monkMessages[4]));
            }
        }
        if(!hasStarted && GameObject.FindGameObjectsWithTag("Instructions").Length == 0) {
            if(currentGamesWon == 0 || currentGamesWon == 3 || currentGamesWon == 6 || currentGamesWon == 11 || currentGamesWon == 14) {
                monkMessageObject.SetActive(true);
            }
            else {
                ContinueGame();
            }
            hasStarted = true;
        }
        if(levelManager.isPlaying()) {
            monkMessageObject.SetActive(false);
        }
    }

    public void MaybeSaySomething() {
        int rand = Random.Range(0, 100);

        if(rand <= 30 && !maybeTextAnimator.GetBool("textVisible")) {
            maybeText.transform.position = maybeTextPositions[Random.Range(0, maybeTextPositions.Count)];
            maybeText.text = gameMessages[Random.Range(0, gameMessages.Count)];

            maybeText.transform.GetChild(0).gameObject.SetActive(true);
            Vector2 textSize = maybeText.GetPreferredValues();
            if(textSize.x > 2.6f) {
                textSize.x = 2.6f;
            }
            maybeText.transform.GetChild(0).gameObject.transform.localScale = textSize;
            
            StartCoroutine("showMessage");
        }
    }

    IEnumerator showMessage() {
        maybeTextAnimator.SetBool("textVisible", true);
        yield return new WaitForSeconds(4);
        maybeTextAnimator.SetBool("textVisible", false);
        maybeText.transform.GetChild(0).gameObject.SetActive(false);
    }
}
