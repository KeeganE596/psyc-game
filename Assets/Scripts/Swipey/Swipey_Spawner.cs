using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_Spawner: Spawns in sparks and gnatts at the appropriate times.
//Stores sparks and Gnatts in object pools to mitigate need to instantiate/destroy objects all the time
//Ability to have different spark and gnatt types
public class Swipey_Spawner : MonoBehaviour
{
    LevelManager levelManager;
    
    List<GameObject> sparkPool;
    public GameObject spark_1;
    public GameObject spark_2;
    public GameObject spark_text;
    int sparkIndex;

    List<GameObject> gnatPool;
    public GameObject gnat_1;
    public GameObject gnat_2;
    public GameObject gnat_fast;
    public GameObject gnat_tank;
    public GameObject gnat_text;
    int gnatIndex;

    bool playing;
    float sparkTimer;
    float sparkSpawnTime;
    float gnatTimer;
    float gnatSpawnTime;

    int currentLevel;

    bool spawnSparks;
    bool spawnGnats;

    List<string> goodWords;
    List<string> badWords;

    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    // Start is called before the first frame update
    void Start() {
        playing = levelManager.isPlaying();
        currentLevel = levelManager.getNumberGamesWon();

        goodWords = new List<string>();
        goodWords.Add("Adventure"); goodWords.Add("Beauty"); goodWords.Add("Caring");
        goodWords.Add("Challenge"); goodWords.Add("Compassion"); goodWords.Add("Connection");
        goodWords.Add("Courage"); goodWords.Add("Creativity"); goodWords.Add("Curiosity");
        goodWords.Add("Encourage"); goodWords.Add("Friendly"); goodWords.Add("Fun");
        goodWords.Add("Excitement"); goodWords.Add("Gratitude"); goodWords.Add("Honesty");
        goodWords.Add("Fitness"); goodWords.Add("Humour"); goodWords.Add("Self-Care");

        badWords = new List<string>();
        badWords.Add("Impatient"); badWords.Add("Anger"); badWords.Add("Disrespect");
        badWords.Add("Can't"); badWords.Add("Dishonest"); badWords.Add("Impossible");
        badWords.Add("Hate");   badWords.Add("Lieing");    badWords.Add("Give-Up");

        // if(GameManager.swipeType == "default") {
            if(levelManager.getIfPlayingChooseGame()) {
                SetupRandomGameSpawn();
            }
            else {
                SetupChooseGameSpawn();
                //ChooseRandomGameType();
            }
        // }
        // else if(GameManager.swipeType == "fast") {
        //     setupFastGnattSpawn();
        // }
        // else if(GameManager.swipeType == "tank") {
        //     setupTankGnattSpawn();
        // }
        // else if(GameManager.swipeType == "normal") {
        //     setupRandomGameSpawn();
        // }
        
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
                gnatTimer += Time.deltaTime;
                if(gnatTimer > gnatSpawnTime) {
                    spawnGnatt();
                    gnatTimer = 0;
                }
            }
        }
    }

    void spawnSpark() {
        if(sparkIndex >= sparkPool.Count) {
            sparkIndex = 0;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(150, Screen.width-300), Random.Range(50, Screen.height-100)));

        //check position isnt too close to middle
        if((spawnPos.x < -2.5 || spawnPos.x > 2.5) && (spawnPos.y < -2.5 || spawnPos.y > 2.5)) {
            if(sparkPool[sparkIndex].activeSelf) {
                sparkIndex++;
                spawnSpark();
            }
            GameObject sp = sparkPool[sparkIndex];
            sp.transform.position = spawnPos;
            sp.SetActive(true);
            sparkIndex++;
        }
        else {
            spawnSpark();
        }
    }

    void spawnGnatt() {
        if(gnatIndex >= gnatPool.Count) {
            gnatIndex = 0;
        }
        
        if(gnatPool[gnatIndex].activeSelf) {
            gnatIndex++;
            spawnGnatt();
        }
        GameObject gn = gnatPool[gnatIndex];
        int side = Random.Range(0, 2);  //pick which side of screen to spawn on
        Vector2 spawnPos;

        if(side == 0) { //if left side of screen
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {  //if right side of screen
            float y = Random.Range(0, Screen.height);
            spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
            gn.GetComponent<Gnatt>().FlipSprite();
        }

        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gnatIndex++;
    }

    void ChooseRandomGameType() {
        // int gameMode = Random.Range(0, 100);    //Pick a game mode: normal, fast gnats or tank gnats 
        // if(gameMode < 15 && levelManager.getNumberGamesWon() > 0) {
        //     setupFastGnattSpawn();
        // }
        // else if(gameMode < 30 && levelManager.getNumberGamesWon() > 0) {
        //     setupTankGnattSpawn();
        // }
        // else {
             SetupRandomGameSpawn();
        // }
    }

    void SetupRandomGameSpawn() {
        if(Random.Range(0, 2) == 0) {   //Choose spark type
            SetupSparkPool(true, "normal");
        }
        else {
            SetupSparkPool(true, "text");
        }
        if(Random.Range(0, 2) == 0) {   //Choose gnat type
            SetupGnatPool(true, "normal", 18);
        }
        else {
            SetupGnatPool(true, "text", 18);
        }
    }

    void SetupChooseGameSpawn() {
        if(currentLevel < 3) {    //Just spawn gnatts for first 3 levels (1-3)
            levelManager.setToWinOnTimeOut();
            SetupSparkPool(false, "none");
            SetupGnatPool(true, "normal", (15 + (currentLevel*2)));
        }
        else if(currentLevel < 6) {    //Just spawn sparks for next 3 levels (4-6)
            //levelManager.setToWinOnTimeOut();
            SetupSparkPool(true, "normal");
            SetupGnatPool(false, "none", 0);
        }
        else if(currentLevel < 11) {  //Spawn both for 5 levels (7-11)
            SetupSparkPool(true, "normal");
            SetupGnatPool(true, "normal", (5 + currentLevel));
        }
        else if(currentLevel < 14) {   //Just spawn text gnats for 3 levels (12-14)
            levelManager.setToWinOnTimeOut();
            SetupSparkPool(false, "none");
            SetupGnatPool(true, "text", (3 + currentLevel));
        }
        else if(currentLevel < 17) {    //Just spawn text sparks for next 3 levels (15-17)
            //levelManager.setToWinOnTimeOut();
            SetupSparkPool(true, "text");
            SetupGnatPool(false, "none", 0);
        }
        else if(currentLevel < 18) {  //Spawn both text types for 3 levels (16-18)
            SetupSparkPool(true, "text");
            SetupGnatPool(true, "text", (currentLevel));
        }
        else {
            SetupRandomGameSpawn();
        }
    }

    void SetupFastGnattSpawn() {
        levelManager.setToWinOnTimeOut();
        SetupSparkPool(false, "normal");
        SetupGnatPool(true, "fast", 15);
    }

    void SetupTankGnattSpawn() {
        levelManager.setToWinOnTimeOut();
        SetupSparkPool(false, "normal");
        SetupGnatPool(true, "tank", 15);
    }

    void SetupGnatPool(bool spawningGnats, string gnatType, int numOfGnats) {
        
        if(!spawningGnats || gnatType == "none") {
            //Do not spawn gnatts
            spawnGnats = false;
            gnatTimer = 0;
            gnatSpawnTime = Mathf.Infinity;
            return;
        }

        //Gnats spawn intialise settings
        spawnGnats = true;
        gnatTimer = 0.1f;
        //gnatSpawnTime = (1.5f - ((currentLevel+1)*0.1f)) >= 0.35f ? 1.5f - ((currentLevel+1)*0.1f) : 0.35f;  //scale gnatt spawn speed depending on level number
        gnatSpawnTime = levelManager.maxTime/numOfGnats;
        float gnatSpeed = PickGnatSpeed();

        //Instantiate gnats to fill object pool with appropriate gnat for the level
        gnatPool = new List<GameObject>();
        //for(int i=0; i<(levelManager.maxTime/gnatSpawnTime); i++) {
        for(int i=0; i<numOfGnats; i++) {
            if(gnatType == "normal") {
                if(Random.Range(0, 2) == 0) { 
                    gnatPool.Add(Instantiate(gnat_1, new Vector2(0, 0), Quaternion.identity)); 
                }
                else { 
                    gnatPool.Add(Instantiate(gnat_2, new Vector2(0, 0), Quaternion.identity)); 
                }
                
            }
            else if(gnatType == "text") {
                gnatPool.Add(Instantiate(gnat_text, new Vector2(0, 0), Quaternion.identity)); 
                gnatPool[i].GetComponent<Gnatt_Text>().SetText(badWords[Random.Range(0, badWords.Count)]);
                
            }
            gnatPool[i].GetComponent<Gnatt>().SetSpeed(gnatSpeed);
            gnatPool[i].SetActive(false);
        }
        gnatIndex = 0;

        //Debug.Log("Gnats: " + gnatPool.Count);
    }
    
    void SetupSparkPool(bool spawningSparks, string sparkType) {
        if(!spawningSparks || sparkType == "none") {
            //Do not spawn sparks
            spawnSparks = false;
            sparkTimer = 0;
            sparkSpawnTime = Mathf.Infinity;
            return;
        }

        //Sparks spawn intialise settings
        spawnSparks = true;
        sparkTimer = 0.1f;
        sparkSpawnTime = levelManager.maxTime/15;
        
        //Instantiate sparks to fill object pool with appropriate sparks for the level
        sparkPool = new List<GameObject>();
        for(int i=0; i<20; i++) {
            if(sparkType == "normal") {
                if(Random.Range(0, 2) == 0) { 
                    sparkPool.Add(Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity)); 
                }
                else { 
                    sparkPool.Add(Instantiate(spark_2, new Vector2(0, 0), Quaternion.identity)); 
                }
            }
            else if(sparkType == "text") {
                sparkPool.Add(Instantiate(spark_text, new Vector2(0, 0), Quaternion.identity));
                sparkPool[i].GetComponent<Spark_Text>().SetText(goodWords[Random.Range(0, goodWords.Count)]);
            }

            sparkPool[i].SetActive(false);
        }
        sparkIndex = 0;
    }

    float PickGnatSpeed() {
        switch (currentLevel) {
            case 0:
                return 0f;
            case 1:
                return 0.2f;
            case 2:
                return 0.4f;
            case 6:
                return 0.2f;
            case 7:
                return 0.3f;
            case 8:
                return 0.4f;
            case 9:
                return 0.5f;
            case 10:
                return 6f;
            case 11:
                return 0.4f;
            case 12:
                return 0.5f;
            case 13:
                return 0.6f;
            default:
                return currentLevel/10f <= 0.8f ? currentLevel/10f : 0.8f;
        }
    }
}
