using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBlob : MonoBehaviour
{
    public GameObject blob;
    private Animator anim;
    public GameObject leftToggle;
    public GameObject rightToggle;
    public GameObject PlayText;
    public GameObject startGameButton;
    public GameObject SettingsText;

    private void Awake()
    {
        PlayText.SetActive(false);
        startGameButton.SetActive(false);
        SettingsText.SetActive(false);
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
        }
        else
        {
            PlayText.SetActive(false);
        }    
    }

    public void Toggle_changedR(bool newValue) {
        anim.SetBool("rightblob", newValue);
        if (newValue)
        {
            StartCoroutine("settingsTextDisplay");
        }
        else {
            SettingsText.SetActive(false);

        }

    }

    IEnumerator playTextDisplay()
    {
        yield return new WaitForSeconds(0.6f);
        PlayText.SetActive(true);
    }

    IEnumerator settingsTextDisplay()
    {
        yield return new WaitForSeconds(0.6f);
        SettingsText.SetActive(true);
    }

}


