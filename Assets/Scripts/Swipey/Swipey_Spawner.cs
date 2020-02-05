using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_Spawner: Spawns in sparks and gnatts at the appropriate times.
//Stores sparks and Gnatts in object pools to mitigate need to instantiate/destroy objects all the time
//Ability to have different spark and gnatt types
public class Swipey_Spawner : MonoBehaviour
{
    List<GameObject> sparks;
    public GameObject spark_1;
    public GameObject spark_2;
    int sparkIndex;

    List<GameObject> gnatts;
    public GameObject gnatt_1;
    public GameObject gnatt_2;
    public GameObject gnatt_fast;
    public GameObject gnatt_tank;
    int gnattIndex;

    LevelManager levelManager;
    bool playing;
    float sparkTimer;
    float sparkSpawnTime;
    float gnattTimer;
    float gnattSpawnTime;

    int levelScaler;

    bool spawnSparks;
    bool spawnGnats;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    // Start is called before the first frame update
    void Start() {
        playing = levelManager.isPlaying();
        levelScaler = levelManager.getNumberGamesWon();

        if(GameManager.swipeType == "default") {
            if(levelManager.getIfPlayingChooseGame()) {
                setupChooseGameSpawn();
            }
            else {
                chooseRandomGameType();
            }
        }
        else if(GameManager.swipeType == "fast") {
            setupFastGnattSpawn();
        }
        else if(GameManager.swipeType == "tank") {
            setupTankGnattSpawn();
        }
        else if(GameManager.swipeType == "normal") {
            setupRandomGameSpawn();
        }
        
    }

    // Update is called once per frame
    void Update() {
        //Check player isn't on instruction screen
        if(levelManager.isPlaying()) {
            if(spawnSparks) {
                sparkTimer += Time.deltaTime;
                if(sparkTimer > sparkSpawnTime) {
                    spawnSpark();
                    sparkTimer = 0;
                }
            }
            if(spawnGnats) {
                gnattTimer += Time.deltaTime;
                if(gnattTimer > gnattSpawnTime) {
                    spawnGnatt();
                    gnattTimer = 0;
                }
            }
        }
    }

    void spawnSpark() {
        if(sparkIndex >= sparks.Count) {
            sparkIndex = 0;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));

        //check position isnt too close to middle
        if((spawnPos.x < -2.5 || spawnPos.x > 2.5) && (spawnPos.y < -2.5 || spawnPos.y > 2.5)) {
            if(sparks[sparkIndex].activeSelf) {
                sparkIndex++;
                spawnSpark();
            }
            GameObject sp = sparks[sparkIndex];
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
        int side = Random.Range(0, 2);  //pick which side of screen to spawn on
        Vector2 spawnPos;

        if(side == 0) { //if left side of screen
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {  //if right side of screen
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
            gn.transform.localScale = new Vector3(-(gn.transform.localScale.x), gn.transform.localScale.y, gn.transform.localScale.z);  //flip sprite
        }

        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gnattIndex++;
    }

    void chooseRandomGameType() {
        int gameMode = Random.Range(0, 100);    //Pick a game mode: normal, fast gnats or tank gnats 
        if(gameMode < 15 && levelManager.getNumberGamesWon() > 0) {
            setupFastGnattSpawn();
        }
        else if(gameMode < 30 && levelManager.getNumberGamesWon() > 0) {
            setupTankGnattSpawn();
        }
        else {
            setupRandomGameSpawn();
        }
    }

    void setupRandomGameSpawn() {
        spawnSparks = true;
        sparkTimer = 0;
        sparkSpawnTime = levelManager.maxTime/15;   //maxtime/neededpoints(10)*1.5
        sparks = new List<GameObject>();
        for(int i=0; i<20; i++) {
            if(Random.Range(0, 2) == 0) { sparks.Add(Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity)); }
            else { sparks.Add(Instantiate(spark_2, new Vector2(0, 0), Quaternion.identity)); }

            sparks[i].SetActive(false);
        }
        sparkIndex = 0;

        spawnGnats = true;
        gnattTimer = 0;
        if(levelScaler < 10) {  //scale gnatt spawn speed depending on level number
            gnattSpawnTime = 2 - ((2*0.9f)*(levelScaler*0.1f));
        }
        else { 
            gnattSpawnTime = 2 - ((2*0.9f)*(9.5f*0.1f)); 
        }
        gnatts = new List<GameObject>();
        for(int i=0; i<(levelManager.maxTime/gnattSpawnTime); i++) {
            if(Random.Range(0, 2) == 0) { gnatts.Add(Instantiate(gnatt_1, new Vector2(0, 0), Quaternion.identity)); }
            else { gnatts.Add(Instantiate(gnatt_2, new Vector2(0, 0), Quaternion.identity)); }
    
            gnatts[i].SetActive(false);
        }
        gnattIndex = 0;
    }

    void setupChooseGameSpawn() {
        if(levelManager.getGamesWon() < 4) {    //Just spawn gnatts for first 4 levels
            levelManager.setToWinOnTimeOut();

            //Do not spawn sparks
            spawnSparks = false;
            sparkTimer = 0;
            sparkSpawnTime = Mathf.Infinity;

            //Gnatt spawn
            spawnGnats = true;
            gnattTimer = (1.5f - ((levelScaler+1)*0.2f)) - 0.2f;
            gnattSpawnTime = 1.5f - ((levelScaler+1)*0.2f);
            //Instantiate Gnatts
            gnatts = new List<GameObject>();
            for(int i=0; i<(levelManager.maxTime/gnattSpawnTime); i++) {
                if(Random.Range(0, 2) == 0) { gnatts.Add(Instantiate(gnatt_1, new Vector2(0, 0), Quaternion.identity)); }
                else { gnatts.Add(Instantiate(gnatt_2, new Vector2(0, 0), Quaternion.identity)); }
        
                gnatts[i].SetActive(false);
            }
            gnattIndex = 0;
        }
        else if(levelManager.getGamesWon() >= 4 && levelManager.getGamesWon() < 8) {    //Just spawn sparks for next 4 levels
            levelManager.setToWinOnTimeOut();
            
            //Do not spawn gnatts
            spawnGnats = false;
            gnattTimer = 0;
            gnattSpawnTime = Mathf.Infinity;

            //Sparks spawn
            spawnSparks = true;
            sparkTimer = 0;
            //sparkSpawnTime = 2f - ((levelScaler+1)*0.1f);
            sparkSpawnTime = ((levelScaler+1)*0.1f) > 0.2f ? 2f - ((levelScaler+1)*0.1f) : 0.2f;
            //Instantiate Sparks
            sparks = new List<GameObject>();
            for(int i=0; i<(levelManager.maxTime/sparkSpawnTime); i++) {
                if(Random.Range(0, 2) == 0) { sparks.Add(Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity)); }
                else { sparks.Add(Instantiate(spark_2, new Vector2(0, 0), Quaternion.identity)); }

                sparks[i].SetActive(false);
            }
            sparkIndex = 0;
        }
        else {  //Spawn both for rest of games
            levelScaler = levelScaler/2;
            chooseRandomGameType();
        }
    }

    void setupFastGnattSpawn() {
        levelManager.setToWinOnTimeOut();

        //Do not spawn sparks
        spawnSparks = false;
        sparkTimer = 0;
        sparkSpawnTime = Mathf.Infinity;

        //Gnatt spawn
        spawnGnats = true;
        gnattTimer = (1.5f - ((levelScaler+1)*0.2f)) - 0.2f;
        //gnattSpawnTime = 1.5f - ((levelScaler+1)*0.5f);
        gnattSpawnTime = ((levelScaler+1)*0.5f) > 0.2f ? 1.5f - ((levelScaler+1)*0.1f) : 0.2f;
        //Instantiate Gnatts
        gnatts = new List<GameObject>();
        for(int i=0; i<(levelManager.maxTime/gnattSpawnTime); i++) {
            gnatts.Add(Instantiate(gnatt_fast, new Vector2(0, 0), Quaternion.identity));
            gnatts[i].SetActive(false);
        }
        gnattIndex = 0;
    }

    void setupTankGnattSpawn() {
        levelManager.setToWinOnTimeOut();
        
        //Do not spawn sparks
        spawnSparks = false;
        sparkTimer = 0;
        sparkSpawnTime = Mathf.Infinity;

        //Gnatt spawn
        spawnGnats = true;
        gnattTimer = (1.5f - ((levelScaler+1)*0.2f)) - 0.2f;
        //gnattSpawnTime = 1.5f - ((levelScaler+1)*0.3f);
        gnattSpawnTime = ((levelScaler+1)*0.3f) > 0.2f ? 1.5f - ((levelScaler+1)*0.1f) : 0.2f;
        //Instantiate Gnatts
        gnatts = new List<GameObject>();
        for(int i=0; i<(levelManager.maxTime/gnattSpawnTime); i++) {
            gnatts.Add(Instantiate(gnatt_tank, new Vector2(0, 0), Quaternion.identity));
            gnatts[i].SetActive(false);
        }
        gnattIndex = 0;
    }
}
