using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipey_Spawner : MonoBehaviour
{
    List<GameObject> sparx;
    public GameObject spark;
    int sparkIndex;

    List<GameObject> gnatts;
    public GameObject gnatt;
    int gnattIndex;

    public GameObject canvas;
    LevelManager levelManager;
    bool playing;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = canvas.GetComponent<LevelManager>();
        //playing = levelManager.isPlaying();
        playing = true;
        timer = 0;

        sparx = new List<GameObject>();
        for(int i=0; i<20; i++) {
            sparx.Add(Instantiate(spark, new Vector2(0, 0), Quaternion.identity));
            sparx[i].SetActive(false);
        }
        sparkIndex = 0;

        gnatts = new List<GameObject>();
        for(int i=0; i<20; i++) {
            gnatts.Add(Instantiate(gnatt, new Vector2(0, 0), Quaternion.identity));
            gnatts[i].SetActive(false);
        }
        gnattIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check player isn't on instruction screen
        if(playing) {
            timer += Time.deltaTime;

            if(timer > 1.2) {
                spawnSpark();
                spawnGnatt();

                timer = 0.5f;
            }
        }
        else {
           // playing = levelManager.isPlaying();
        }
    }

    void spawnSpark() {
        //Get rendom position to spawn word at
         Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));

        //check position isnt too close to middle
        if((spawnPos.x < -0.75 || spawnPos.x > 0.75) && (spawnPos.y < -0.75 || spawnPos.y > 0.75)) {
            GameObject sp = sparx[sparkIndex];
            sp.transform.position = spawnPos;
            sp.SetActive(true);
            sparkIndex++;

            if(sparkIndex >= sparx.Count && sparx[0].activeSelf) { sparkIndex = 0; }
        }
    }

    void spawnGnatt() {
        int side = Random.Range(0, 2);
        Vector2 spawnPos;

        if(side == 0) {
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
        }

        GameObject gn = gnatts[gnattIndex];
        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gnattIndex++;

        if(gnattIndex >= gnatts.Count && gnatts[0].activeSelf) { gnattIndex = 0; }
    }
}
