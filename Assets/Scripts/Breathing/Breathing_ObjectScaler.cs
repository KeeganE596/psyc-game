using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Breathing_ObjectScaler: scale the limit circles depending on the level number and screen res
public class Breathing_ObjectScaler : MonoBehaviour
{
    LevelManager levelManager;
    public GameObject expandBlob;
    public GameObject breatheLimitParent;
    public GameObject scoreBar;

    Vector3 worldScale;
    float levelScaler;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        
        if(GameManagerStatic.GetCurrentLevelNumber() < 6) {
            levelScaler = GameManagerStatic.GetCurrentLevelNumber()*0.02f;
        }
        else { levelScaler = 6*0.02f; }

        ScaleObjects();
        ScaleScoreBar();

        Color32 bgColor = ColorManager.GetColor();
        breatheLimitParent.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = bgColor;
        breatheLimitParent.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>().color = bgColor;
    }

    void ScaleObjects() {
        breatheLimitParent.transform.GetChild(0).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.76f)/2, (worldScale.y*0.76f)/2, 0);
        breatheLimitParent.transform.GetChild(1).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.75f)/2, (worldScale.y*0.75f)/2, 0);
        breatheLimitParent.transform.GetChild(2).gameObject.transform.localScale = 
                new Vector3((worldScale.y*(0.6f+levelScaler))/2, (worldScale.y*(0.6f+levelScaler))/2, 0);
        breatheLimitParent.transform.GetChild(3).gameObject.transform.localScale = 
                new Vector3((worldScale.y*(0.4f-levelScaler))/2, (worldScale.y*(0.4f-levelScaler))/2, 0);
        breatheLimitParent.transform.GetChild(4).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.25f)/2, (worldScale.y*0.25f)/2, 0);
        breatheLimitParent.transform.GetChild(5).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.25f)/2, (worldScale.y*0.25f)/2, 0);
        breatheLimitParent.transform.GetChild(6).gameObject.transform.localScale = 
                new Vector3((worldScale.y*0.24f)/2, (worldScale.y*0.24f)/2, 0);
    }

    void ScaleScoreBar() {
        scoreBar.transform.GetChild(0).gameObject.transform.position = new Vector3(0-((2*worldScale.x)/3), 0+(worldScale.y*0.5f), 0);
        scoreBar.transform.GetChild(0).gameObject.transform.localScale = new Vector3(worldScale.y*0.04f, worldScale.y*0.04f, 0);
        scoreBar.transform.GetChild(1).gameObject.transform.position = new Vector3(0-((2*worldScale.x)/3), 0-(worldScale.y*0.5f), 0);
        scoreBar.transform.GetChild(1).gameObject.transform.localScale = new Vector3(worldScale.y*0.04f, worldScale.y*0.04f, 0);
        scoreBar.transform.GetChild(2).gameObject.transform.position = new Vector3(0-((2*worldScale.x)/3), 0, 0);
        scoreBar.transform.GetChild(2).gameObject.transform.localScale = new Vector3(worldScale.y*0.1f, (worldScale.y*0.5f), 0);
    }
}
