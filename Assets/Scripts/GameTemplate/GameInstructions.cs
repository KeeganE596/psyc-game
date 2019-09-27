using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstructions : MonoBehaviour
{

    GameTimer timerScript;

    // Start is called before the first frame update
    void Start()
    {
        timerScript = GetComponent<GameTimer>();
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine("readInstructionsWait");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator readInstructionsWait()  {
        yield return new WaitForSeconds(5f);
        StartGame();
    }

    public void StartGame() {
        GameObject instructions  = this.gameObject.transform.GetChild(0).gameObject;
        instructions.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        timerScript.startTimerNow();
    }
}
