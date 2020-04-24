using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickGnat : MonoBehaviour
{
    List<string> gnats;
    Color32 onColor = new Color32(74, 156, 48, 255);
    Color32 offColor = new Color32(74, 156, 48, 0);
    string clickedType;
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start() {
        gnats = new List<string>();
        continueButton.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ClickedGnat(string type) {
        if(gnats.Contains(type)) {
            gnats.Remove(type);
        }
        else if(gnats.Count < 2) {
            gnats.Add(type);
        }

        clickedType = type;

        if(gnats.Count == 1) {
            PlayerPrefs.SetString("gnat1", gnats[0]);
            continueButton.SetActive(false);
        }
        else if(gnats.Count == 2) {
            PlayerPrefs.SetString("gnat1", gnats[0]);
            PlayerPrefs.SetString("gnat2", gnats[1]);
            continueButton.SetActive(true);
        }
    }

    public void SwitchSelectRing(SpriteRenderer ring) {
        if(gnats.Contains(clickedType)) {
            ring.color = onColor;
        }
        else {
            ring.color = offColor;
        }
    }

    public void Continue() {
        PlayerPrefs.Save();
        SceneManager.LoadScene("swipeAway_Game");
    }
}
