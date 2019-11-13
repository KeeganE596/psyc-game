using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipey_Spawner : MonoBehaviour
{
    List<GameObject> sparx;
    public GameObject spark_1;
    public GameObject spark_2;
    int sparkIndex;

    List<GameObject> gnatts;
    public GameObject gnatt_1;
    public GameObject gnatt_2;
    int gnattIndex;

    public GameObject canvas;
    LevelManager levelManager;
    bool playing;
    float sparkTimer;
    float sparkSpawnTime;
    float gnattTimer;
    float gnattSpawnTime;

    int levelScaler;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    // Start is called before the first frame update
    void Start() {
        playing = levelManager.isPlaying();
        levelScaler = levelManager.getNumberGamesWon();

        sparkTimer = 0;
        sparkSpawnTime = levelManager.maxTime/15;   //maxtime/neededpoints(10)*1.5
        gnattTimer = 0;
        if(levelScaler < 10) {  //scale gnatt spawn speed depending on level number
            gnattSpawnTime = 2 - ((2*0.9f)*(levelScaler*0.1f));
        }
        else { gnattSpawnTime = 2 - ((2*0.9f)*(9.5f*0.1f)); }

        sparx = new List<GameObject>();
        int sparkNum = 0;
        for(int i=0; i<20; i++) {
            sparkNum = Random.Range(0, 2);
            if(sparkNum == 0) { sparx.Add(Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity)); }
            else if(sparkNum == 1) { sparx.Add(Instantiate(spark_2, new Vector2(0, 0), Quaternion.identity)); }

            sparx[i].SetActive(false);
        }
        sparkIndex = 0;

        gnatts = new List<GameObject>();
        int gnattNum = 0;
        for(int i=0; i<(levelManager.maxTime/gnattSpawnTime); i++) {
            gnattNum = Random.Range(0, 2);
            if(gnattNum == 0) { gnatts.Add(Instantiate(gnatt_1, new Vector2(0, 0), Quaternion.identity)); }
            else if(gnattNum == 1) { gnatts.Add(Instantiate(gnatt_2, new Vector2(0, 0), Quaternion.identity)); }
    
            gnatts[i].SetActive(false);
        }
        gnattIndex = 0;
    }

    // Update is called once per frame
    void Update() {
        //Check player isn't on instruction screen
        if(levelManager.isPlaying()) {
            sparkTimer += Time.deltaTime;
            gnattTimer += Time.deltaTime;

            if(sparkTimer > sparkSpawnTime) {
                spawnSpark();
                sparkTimer = 0;
            }
            if(gnattTimer > gnattSpawnTime) {
                spawnGnatt();
                gnattTimer = 0;
            }
        }
    }

    void spawnSpark() {
        if(sparkIndex >= sparx.Count) {
            sparkIndex = 0;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));

        //check position isnt too close to middle
        if((spawnPos.x < -0.75 || spawnPos.x > 0.75) && (spawnPos.y < -0.75 || spawnPos.y > 0.75)) {
            if(sparx[sparkIndex].activeSelf) {
                sparkIndex++;
                spawnSpark();
            }
            GameObject sp = sparx[sparkIndex];
            sp.transform.position = spawnPos;
            sp.SetActive(true);
            sparkIndex++;
        }
        else {
            spawnSpark();
        }
    }

    void spawnGnatt() {
        if(gnattIndex >= gnatts.Count) {
            gnattIndex = 0;
        }

        if(gnatts[gnattIndex].activeSelf) {
            gnattIndex++;
            spawnGnatt();
        }
        GameObject gn = gnatts[gnattIndex];
        int side = Random.Range(0, 2);
        Vector2 spawnPos;

        if(side == 0) {
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
            gn.transform.localScale = new Vector3(-(gn.transform.localScale.x), gn.transform.localScale.y, gn.transform.localScale.z);
        }

        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gnattIndex++;

    }
}
