using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Swipey_MessageController : MessageController
{
    public GameObject textObject;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        ContinueGame();
    }

    // Update is called once per frame
    void Update() {
        // if(!levelManager.getIfPlayingChooseGame() && !hasStarted && currentGamesWon == 0) {
        //     string text = "Swipe the <b>GNATs</b> away to relax your Brain.";
        //     StartCoroutine(ShowGameText(text));
        //     hasStarted = true;
        // }
        // else if (!hasStarted) {
        //     hasStarted = true;
        //     ContinueGame();
        //     textObject.SetActive(false);
        // }
    }

    protected IEnumerator ShowGameText(string gameText) {
        textObject.GetComponentInChildren<TextMeshPro>().text = gameText;
        yield return new WaitForSeconds(4f);
        ContinueGame();
        textObject.SetActive(false);
    }
}
