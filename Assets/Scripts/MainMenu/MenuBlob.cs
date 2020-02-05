using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBlob : MonoBehaviour
{
    public GameObject blob;
    private Animator anim;
    public GameObject PlayText;
    public GameObject SettingsText;
    public GameObject Toggle_allButton;
    public GameObject customizePanel;
    public GameObject aboutPanel;
    public GameObject leaderboardPanel;

    public GameObject startGameButton;
    public GameObject startChooseGameButton;
    public GameObject customizeButton;
    public GameObject aboutButton;
    public GameObject leaderboardButton;
    public GameObject settingsText;
    public GameObject informationPanel;




    private void Awake() {
        PlayText.SetActive(false);
        startGameButton.SetActive(false);
        startChooseGameButton.SetActive(false);
        SettingsText.SetActive(false);
        Toggle_allButton.SetActive(false);
        customizeButton.SetActive(false);
        aboutButton.SetActive(false);
    }


    private void Start() {
        anim = blob.GetComponent<Animator>();
    }

    public void TogglePlay(bool newValue) {
        anim.SetBool("playblob", newValue);
        if(newValue) {
            StartCoroutine("playTextDisplay");
            Toggle_allButton.SetActive(true);
        }
        else {
            PlayText.SetActive(false);
            Toggle_allButton.SetActive(false);
            startGameButton.SetActive(false);
            startChooseGameButton.SetActive(false);
        }    
    }

    IEnumerator playTextDisplay()
    {
        yield return new WaitForSeconds(0.3f);
        startGameButton.SetActive(true);
        startChooseGameButton.SetActive(true);
        //PlayText.SetActive(true);
    }

    public void toggle_all(bool newValue) {
            anim.SetBool("customizeblob", !newValue);
            anim.SetBool("playblob", !newValue);
            anim.SetBool("centerblob", !newValue);
            anim.SetBool("aboutblob", !newValue);
            Toggle_allButton.SetActive(false);
            startGameButton.SetActive(false);
            startChooseGameButton.SetActive(false);
            customizeButton.SetActive(false);
            aboutButton.SetActive(false);
            SettingsText.SetActive(false);
            PlayText.SetActive(false);
            customizePanel.SetActive(false);
            aboutPanel.SetActive(false);
            leaderboardPanel.SetActive(false);
    }

    public void toggleCustomizePanel(bool newValue) {
        Toggle_allButton.SetActive(true);
        anim.SetBool("centerblob", newValue);
        StartCoroutine("WaitToDisplayCustomize");
    }

    public void toggleAboutPanel(bool newValue) {
        Toggle_allButton.SetActive(true);
        anim.SetBool("centerblob", newValue);
        StartCoroutine("WaitToDisplayAbout");
    }

    public void toggleLeaderboardPanel(bool newValue) {
        Toggle_allButton.SetActive(true);
        anim.SetBool("centerblob", newValue);
        StartCoroutine("WaitToDisplayLeaderboard");
    }

    IEnumerator WaitToDisplayCustomize() {
        yield return new WaitForSeconds(0.4f);
        customizeButton.SetActive(false);
        SettingsText.SetActive(false);
        customizePanel.SetActive(true);
    }

    IEnumerator WaitToDisplayAbout() {
        yield return new WaitForSeconds(0.4f);
        aboutButton.SetActive(false);
        aboutPanel.SetActive(true);
    }

    IEnumerator WaitToDisplayLeaderboard() {
        yield return new WaitForSeconds(0.4f);
        leaderboardButton.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}



