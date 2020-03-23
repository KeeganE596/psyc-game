using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_MessageController : MessageController
{
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        if(levelManager.isPlaying() && levelManager.getGamesWon() < 1 && !hasStarted) {
            hasStarted = true;
            StartCoroutine("CycleGameText");
        }
        if(levelManager.isPlaying()) {
            monkMessageObject.SetActive(false);
        }
    }

    IEnumerator CycleGameText() {
        // messageTextObj.SetActive(true);
        // messageText.text = "Breathe in as the circle expands, hold your finger down and your breathe when it's in the green";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = "Let go and breathe out as the circle contracts, hold down again when it's in the green";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = "Continue your steady breathing to help calm your body";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // textAnimator.SetTrigger("fadeOut");
         yield return new WaitForSeconds(1f);
        // messageTextObj.SetActive(false);
    }
}
