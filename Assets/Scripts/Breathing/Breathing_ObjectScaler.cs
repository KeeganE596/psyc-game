using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Breathing_ObjectScaler: scale the limit circles depending on the level number and screen res
public class Breathing_ObjectScaler : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject expandBlob;
    public GameObject breatheLimitParent;

    Vector3 worldScale;
    float levelScaler;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        
        if(levelManager.getNumberGamesWon() < 6) {
            levelScaler = levelManager.getNumberGamesWon()*0.02f;
        }
        else { levelScaler = 6*0.02f; }

        ScaleObjects();
    }

    void ScaleObjects() {
        breatheLimitParent.transform.GetChild(0).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.75f)/2, (worldScale.y*0.75f)/2, 0);
        breatheLimitParent.transform.GetChild(1).gameObject.transform.localScale = 
                new Vector3((worldScale.y*(0.6f+levelScaler))/2, (worldScale.y*(0.6f+levelScaler))/2, 0);
        breatheLimitParent.transform.GetChild(2).gameObject.transform.localScale = 
                new Vector3((worldScale.y*(0.4f-levelScaler))/2, (worldScale.y*(0.4f-levelScaler))/2, 0);
        breatheLimitParent.transform.GetChild(3).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.25f)/2, (worldScale.y*0.25f)/2, 0);
    }
}
