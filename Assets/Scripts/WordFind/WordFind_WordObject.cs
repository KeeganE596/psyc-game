using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordFind_WordObject : MonoBehaviour
{
    WordFind_SelectWords selectWordsScript;
    bool isSelected = false;
    TextMeshPro wordText;
    SpriteRenderer wordBackground;

    void Awake() {
        selectWordsScript = GameObject.FindWithTag("SelectedWords").GetComponent<WordFind_SelectWords>();

        wordText = this.GetComponentInChildren<TextMeshPro>();
        wordBackground = this.GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void IsClicked() {
        if(isSelected) {  //unselect word
            selectWordsScript.RemoveWordFromSelected(wordText.text);
            wordText.color = new Color32(255, 255, 255, 255);
            wordBackground.color = new Color32(0, 0, 0, 50);
            isSelected = false;
        }
        else {  //select word
            if(selectWordsScript.AddWordToSelected(wordText.text)) {
                wordText.color = new Color32(50, 50, 50, 255);
                wordBackground.color = new Color32(255, 255, 255, 255);
                isSelected = true;
            }
        }
        
    }

    public void SetText(string word) {
        wordText.text = word;
    }

    public string GetText() {
        return wordText.text;
    }

    public bool GetIfSelected() {
        return isSelected;
    }
}
