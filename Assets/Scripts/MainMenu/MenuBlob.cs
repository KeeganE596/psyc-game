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
    public GameObject settingsPanel;

    public GameObject startGameButton;
    public GameObject settingsButton;
    public GameObject settingsText;
    public GameObject informationPanel;




    private void Awake()
    {
        PlayText.SetActive(false);
        startGameButton.SetActive(false);
        SettingsText.SetActive(false);
        Toggle_allButton.SetActive(false);
        settingsButton.SetActive(false);
    }


    private void Start()
    {
        anim = blob.GetComponent<Animator>();
    }

    public void Toggle_changedL(bool newValue) {
        anim.SetBool("leftblob", newValue);
        if(newValue)
        {
            StartCoroutine("playTextDisplay");
            Toggle_allButton.SetActive(true);
        }
        else
        {
            Toggle_allButton.SetActive(false);
            startGameButton.SetActive(false);
            settingsButton.SetActive(false);
            PlayText.SetActive(false);
        }    
    }

    public void Toggle_changedR(bool newValue) {
        anim.SetBool("rightblob", newValue);
        if (newValue)
        {
            StartCoroutine("settingsTextDisplay");
            Toggle_allButton.SetActive(true);

        }
        else {
            SettingsText.SetActive(false);
            Toggle_allButton.SetActive(false);
            settingsButton.SetActive(false);
        }

    }

    IEnumerator playTextDisplay()
    {
        yield return new WaitForSeconds(0.4f);
        startGameButton.SetActive(true);
        PlayText.SetActive(true);
    }

    IEnumerator settingsTextDisplay()
    {
        yield return new WaitForSeconds(0.4f);
        SettingsText.SetActive(true);
        settingsButton.SetActive(true);
    }

    public void toggle_all(bool newValue) {
            anim.SetBool("rightblob", !newValue);
            anim.SetBool("leftblob", !newValue);
            anim.SetBool("centerblob", !newValue);
            startGameButton.SetActive(false);
            SettingsText.SetActive(false);
            PlayText.SetActive(false);
            Toggle_allButton.SetActive(false);
            settingsButton.SetActive(false);
            settingsPanel.SetActive(false);



    }

    public void toggleSettingsPanel(bool newValue)
    {
        anim.SetBool("centerblob", newValue);
        settingsButton.SetActive(false);
        SettingsText.SetActive(false);
        settingsPanel.SetActive(true);

    }


    public void Toggle_InformationPanel(bool newValue)
    {

        anim.SetBool("showInfo", newValue);
    }
    public void Close_InformationPanel(bool newValue)
    {
        anim.SetBool("showInfo", newValue);
    }

    public void showSettingsPanel(bool newValue)
    {
        settingsPanel.SetActive(newValue);
        informationPanel.SetActive(newValue);

    }
}



