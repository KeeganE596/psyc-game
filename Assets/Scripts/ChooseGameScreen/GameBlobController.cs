using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBlobController : MonoBehaviour
{
    GameManager gameManager;

    Animator anim;
    GameObject centerBlob;
    TextMeshPro gameNameText;
    TextMeshPro gameCopyText;

    string currentGame;

    void Awake() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start() {
        anim = this.GetComponent<Animator>();
        centerBlob = this.transform.GetChild(1).GetChild(0).gameObject;
        gameNameText = centerBlob.transform.GetChild(0).GetComponent<TextMeshPro>();
        gameCopyText = centerBlob.transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void GameBlobClicked(string name) {
        anim.SetBool("gameBlobClicked", true);
        currentGame = name;
        switch(name) {
            case "swipeAway": 
                gameNameText.text = "Swipey";
                gameCopyText.text = "Tap on the Sparx  to collect them and earn points, swipe away the Gnatts or you will lose points.";
                break;
            case "breathing": 
                gameNameText.text = "Breathing";
                gameCopyText.text = "Breathe in as the circle expands, tap and hold when its in the green, breathe out as the circle contracts and hold again when its in the green.";
                break;
            case "wordAssociation": 
                gameNameText.text = "Words";
                gameCopyText.text = "Drag from the middle circle and connect to the positive words, avoid the negative words or you'll break all of your connections.";
                break;
            case "inspection": 
                gameNameText.text = "Thoughts";
                gameCopyText.text = "Tap each thought and decide whether it's a good thought or one you can dismiss by tapping the red or green buttons.";
                break;
            default:
                gameNameText.text = "Game Name";
                gameCopyText.text = "Game explaination copy";
                break;
        }
    }

    public void UnclickGameBlob() {
        anim.SetBool("gameBlobClicked", false);
    }

    public void PlayGame() {
        gameManager.PickGame(currentGame);
    }

    public void ToMainMenu() {
        StartCoroutine("WaitToGoToMenu");
    }

    IEnumerator WaitToGoToMenu() {
        anim.SetTrigger("despawnBlob");
        yield return new WaitForSeconds(0.7f);
        gameManager.ToMainMenu();
    }
}
