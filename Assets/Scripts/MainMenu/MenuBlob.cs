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

    public GameObject startGameButton;
    public GameObject startChooseGameButton;
    public GameObject customizeButton;
    public GameObject aboutButton;
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

    public void ToggleCustomize(bool newValue) {
        anim.SetBool("customizeblob", newValue);
        if (newValue) {
            StartCoroutine("settingsTextDisplay");
            Toggle_allButton.SetActive(true);

        }
        else {
            SettingsText.SetActive(false);
            Toggle_allButton.SetActive(false);
            customizeButton.SetActive(false);
        }

    }

    public void ToggleAbout(bool newValue) {
        anim.SetBool("aboutblob", newValue);
        if (newValue) {
            StartCoroutine("aboutTextDisplay");
            Toggle_allButton.SetActive(true);
        }
        else {
            Toggle_allButton.SetActive(false);
            aboutButton.SetActive(false);
        }

    }

    IEnumerator playTextDisplay()
    {
        yield return new WaitForSeconds(0.3f);
        startGameButton.SetActive(true);
        startChooseGameButton.SetActive(true);
        //PlayText.SetActive(true);
    }

    IEnumerator settingsTextDisplay()
    {
        yield return new WaitForSeconds(0.3f);
        SettingsText.SetActive(true);
        customizeButton.SetActive(true);
    }

    IEnumerator aboutTextDisplay()
    {
        yield return new WaitForSeconds(0.3f);
        //SettingsText.SetActive(true);
        aboutButton.SetActive(true);
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
    }

    public void toggleCustomizePanel(bool newValue) {
        Toggle_allButton.SetActive(true);
        anim.SetBool("centerblob", newValue);
        customizeButton.SetActive(false);
        SettingsText.SetActive(false);
        customizePanel.SetActive(true);
    }

    public void toggleAboutPanel(bool newValue) {
        Toggle_allButton.SetActive(true);
        anim.SetBool("centerblob", newValue);
        aboutButton.SetActive(false);
        //SettingsText.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void Toggle_InformationPanel(bool newValue) {
        anim.SetBool("showInfo", newValue);
    }
    public void Close_InformationPanel(bool newValue) {
        anim.SetBool("showInfo", newValue);
    }

    public void showSettingsPanel(bool newValue) {
        customizePanel.SetActive(newValue);
        informationPanel.SetActive(newValue);

    }
}



