using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    [SerializeField] private IntroductionGameManager IntroductionGameManager;
    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private GameObject scene_3_gnats;
    [SerializeField] private GameObject scene_4_sparx;
    [SerializeField] private GameObject player;

    private int currentScene = 0;

    void Start() {
        scene_3_gnats.SetActive(false);
        scene_4_sparx.SetActive(false);
        player.SetActive(false);
        NextScene();
    }

    public void NextScene() {
        for(int i=0; i<scenes.Count; i++) {
            if(i == currentScene) {
                scenes[i].SetActive(true);

                if(currentScene == 3)
                    scene_3_gnats.SetActive(true);
                if(currentScene == 4) {
                    scene_3_gnats.SetActive(false);
                    scene_4_sparx.SetActive(true);
                }
                if(currentScene == 5) {
                    scene_4_sparx.SetActive(false);
                    player.SetActive(true);
                    IntroductionGameManager.StartPlaying();
                }
            }
            else {
                scenes[i].SetActive(false);
                player.SetActive(false);
            }
        }
        currentScene++;
    }
}
