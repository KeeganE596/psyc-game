using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection_MessageController : MessageController
{
    // Start is called before the first frame updat
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {
        if(levelManager.isPlaying() && GameManagerStatic.GetCurrentLevelNumber() < 1 && !hasStarted) {
            hasStarted = true;
            //StartCoroutine("CycleGameText");
        }
    }

    IEnumerator CycleGameText() {
        // messageTextObj.SetActive(true);
        // messageText.text = "Thoughts come and go";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = "Some thoughts are helpful and some are not so helpful...this is normal";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = "Try stepping back and having a look at these ones";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // messageText.text = "Decide which thoughts are helpful by using the buttons";
        // textAnimator.SetTrigger("showText");
        // yield return new WaitForSeconds(6f);

        // textAnimator.SetTrigger("fadeOut");
         yield return new WaitForSeconds(1f);
        // messageTextObj.SetActive(false);
    }
}
