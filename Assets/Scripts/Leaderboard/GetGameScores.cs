using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetGameScores : MonoBehaviour
{
    public TextMeshProUGUI swipey;
    public TextMeshProUGUI breathing;
    public TextMeshProUGUI words;
    public TextMeshProUGUI thoughts;

    // Start is called before the first frame update
    void Start()
    {
        GetScores("swipeAway_Game", swipey);
        GetScores("breathing_Game", breathing);
        GetScores("wordAssociation_Game", words);
        GetScores("inspection_Game", thoughts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetScores(string scene, TextMeshProUGUI textObj) {
        int score = 0;
        if(PlayerPrefs.HasKey(scene)) {
            score = PlayerPrefs.GetInt(scene);
            textObj.text = textObj.text + score + " levels";
        }
        else {
            score = 0;
            textObj.text = textObj.text + "-";
        }
    }
}
