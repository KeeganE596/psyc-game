using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    [SerializeField] private IntroductionGameManager IntroGameManager;
    [SerializeField] private GameObject scene_1;
    [SerializeField] private GameObject scene_2;
    [SerializeField] private GameObject scene_3;
    [SerializeField] private GameObject scene_3_gnats;
    [SerializeField] private GameObject scene_4;
    [SerializeField] private GameObject scene_4_sparx;
    [SerializeField] private GameObject scene_5;
    [SerializeField] private GameObject player;

    void Start() {
        NextScene(1);
    }

    public void NextScene(int num) {
        scene_1.SetActive(false);
        scene_2.SetActive(false);
        scene_3.SetActive(false);
        scene_3_gnats.SetActive(false);
        scene_4.SetActive(false);
        scene_4_sparx.SetActive(false);
        scene_5.SetActive(false);
        player.SetActive(false);

        switch(num) {
            case 1:
                scene_1.SetActive(true);
                break;
            case 2:
                scene_2.SetActive(true);
                break;
            case 3:
                scene_3.SetActive(true);
                scene_3_gnats.SetActive(true);
                break;
            case 4:
                scene_4.SetActive(true);
                scene_4_sparx.SetActive(true);
                break;
            case 5:
                scene_5.SetActive(true);
                player.SetActive(true);
                IntroGameManager.StartPlaying();
                break;
        }   
    }
}
