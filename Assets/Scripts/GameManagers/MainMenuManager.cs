using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject blob;
    private Animator anim;
    [SerializeField] private GameObject toggleAllButton;
    [SerializeField] private GameObject customizePanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject playPanel;

    private void Awake() {
        anim = blob.GetComponent<Animator>();
    }

    private void Start() {
        SaveManager.CheckForPlayerPrefs();
        toggleAllButton.SetActive(false);
    }

    public void ToggleAllPanelsOff() {
        anim.SetBool("customizeblob", false);
        anim.SetBool("playblob", false);
        anim.SetBool("centerblob", false);
        anim.SetBool("aboutblob", false);
        
        toggleAllButton.SetActive(false);
        customizePanel.SetActive(false);
        aboutPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void TogglePanelOn(string panel) {
        toggleAllButton.SetActive(true);
        anim.SetBool("centerblob", true);
        StartCoroutine(WaitToDisplayPanel(panel));
    }

    public void StartGame(string type) {
        anim.SetBool("centerblob", true);
        StartCoroutine(WaitToStartGame(type));
    }

    IEnumerator WaitToDisplayPanel(string panel) {
        yield return new WaitForSeconds(0.4f);

        switch(panel) {
            case "play":
                playPanel.SetActive(true);
                break;
            case "customize":
                customizePanel.SetActive(true);
                break;
            case "about":
                aboutPanel.SetActive(true);
                break;
            case "leaderboard":
                leaderboardPanel.SetActive(true);
                break;
        }
    }

    IEnumerator WaitToStartGame(string type) {
        anim.SetTrigger("exitblob");
        playPanel.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().StartGame(type);
        GameManagerStatic.ChooseGameType(type);
    }
}
