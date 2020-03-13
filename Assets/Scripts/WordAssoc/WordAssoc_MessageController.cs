using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordAssoc_MessageController : MessageController
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();

        //ContinueGame();
    }

    // Update is called once per frame
    void Update() {
        //if(levelManager.getIfPlayingChooseGame() && hasStarted) {
            //if(currentGamesWon == 0) {
                //StartCoroutine(CycleGameTextSingle(gnattMessages[0]));
            //}
        //}
        if(!hasStarted && GameObject.FindGameObjectsWithTag("Instructions").Length == 0) {
            if(!levelManager.getIfPlayingChooseGame() && currentGamesWon == 31) {
                monkMessageObject.SetActive(true);
                StartCoroutine(CycleGameTextSingle("There are things we can do to help chill the Zen Ninja. Connect strategies that can help the Zen Ninja relax"));
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
}
