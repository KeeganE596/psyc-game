using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds_MessageController : MessageController
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        if(!hasStarted && GameObject.FindGameObjectsWithTag("Instructions").Length == 0) {
            if(!levelManager.getIfPlayingChooseGame() && currentGamesWon == 31) {
                monkMessageObject.SetActive(true);
                StartCoroutine(CycleGameTextDouble("When things are bit grey in our life…it can affect our thoughts, feelings and behaviours...",
                                                    "Collect the SPARX and connect the strategies to lighten up the day!"));
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
