using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    [SerializeField] private IntroductionGameManager IntroductionGameManager;
    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private GameObject scene_2_gnats;
    [SerializeField] private GameObject scene_3_sparx;
    [SerializeField] private GameObject player;

    void Start() {
        scene_2_gnats.SetActive(false);
        scene_3_sparx.SetActive(false);
        player.SetActive(false);
        NextScene(0);
    }

    public void NextScene(int num) {
        for(int i=0; i<scenes.Count; i++) {
            if(i == num) {
                scenes[i].SetActive(true);

                if(num == 2)
                    scene_2_gnats.SetActive(true);
                if(num == 3) {
                    scene_2_gnats.SetActive(false);
                    scene_3_sparx.SetActive(true);
                }
                if(num == 4) {
                    scene_3_sparx.SetActive(false);
                    player.SetActive(true);
                    IntroductionGameManager.StartPlaying();
                }
            }
            else {
                scenes[i].SetActive(false);
                player.SetActive(false);
            }
        }
    }
}
