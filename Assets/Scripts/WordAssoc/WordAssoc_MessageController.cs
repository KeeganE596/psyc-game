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
            if(GameManagerStatic.GetPlayingRandomGame() && currentGamesWon == 25) {
                monkMessageObject.SetActive(true);
                // StartCoroutine(CycleGameTextDouble("There are things we can do to help chill the Zen Ninja, doing something enjoyable helps our mental health...",
                //                                     "Tap and drag to connect strategies to the Zen Ninja that can help him relax"));
                List<string> textList = new List<string>();
                textList.Add("There are things that we can do to help chill out the Zen Ninja...");
                textList.Add("Doing something enjoyable helps our mental health...");
                textList.Add("Tap and drag to connect strategies to the Zen Ninja that can help him relax.");
                StartCoroutine(CycleGameText(textList));
            }
            else {
                ContinueGame();
            }
            hasStarted = true;
        }
        if(LevelManager.Instance.GetIfGameIsPlaying()) {
            monkMessageObject.SetActive(false);
        }
    }
}
