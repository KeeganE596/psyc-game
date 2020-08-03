using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_Spawner: Spawns in sparks and gnatts at the appropriate times.
//Stores sparks and Gnatts in object pools to mitigate need to instantiate/destroy objects all the time
//Ability to have different spark and gnatt types
public class Swipey_Spawner : MonoBehaviour
{
    LevelManager levelManager;
    
    //Sparx
    Queue<GameObject> sparkPoolQu;
    public GameObject spark_1;
    //public GameObject spark_2;
    public GameObject spark_text;
    int sparkIndex;

    //Gnats
    Queue<GameObject> gnatPool;
    public GameObject gnat_normal;
    public GameObject gnat_text;

    bool playing;

    //Timers
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
        currentLevel = GameManagerStatic.GetCurrentLevelNumber();

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
            if(GameManagerStatic.GetPlayingRandomGame()) {
                SetupRandomGameSpawn();
            }
            else {
                //Debug.Log("Choose");
                GetLevelSpawns();
                //SetupChooseGameSpawn();
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
                    spawnGnat();
                    gnatTimer = 0;
                }
            }
        }
    }


    void spawnSpark() {
        if(sparkPoolQu.Count == 0) {
            return;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(100, Screen.width-100), Random.Range(75, Screen.height-120)));

        //check position isnt too close to middle
        if((spawnPos.x < -2.5 || spawnPos.x > 2.5) && (spawnPos.y < -2.5 || spawnPos.y > 2.5)) {
            GameObject sp = sparkPoolQu.Dequeue();
            sp.transform.position = spawnPos;
            sp.SetActive(true);
        }
        else {
            spawnSpark();
        }
    }

    void spawnGnat() {
        if(gnatPool.Count == 0) {
            Debug.Log("No Gnats to spawn");
            return;
        }

        GameObject gn = gnatPool.Dequeue();
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
        gn.GetComponent<Gnatt>().ChooseSprite();
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

    // void SetupChooseGameSpawn() {
    //     if(currentLevel < 3) {    //Just spawn gnatts for first 3 levels (1-3)
    //         levelManager.setToWinOnTimeOut();
    //         SetupSparkPool(false, "none");
    //         SetupGnatPool(true, "normal", (15 + (currentLevel*2)));
    //     }
    //     else if(currentLevel < 6) {    //Just spawn sparks for next 3 levels (4-6)
    //         //levelManager.setToWinOnTimeOut();
    //         SetupSparkPool(true, "normal");
    //         SetupGnatPool(false, "none", 0);
    //     }
    //     else if(currentLevel < 11) {  //Spawn both for 5 levels (7-11)
    //         SetupSparkPool(true, "normal");
    //         SetupGnatPool(true, "normal", (4 + currentLevel));
    //     }
    //     else if(currentLevel < 14) {   //Just spawn text gnats for 3 levels (12-14)
    //         levelManager.setToWinOnTimeOut();
    //         SetupSparkPool(false, "none");
    //         SetupGnatPool(true, "text", (3 + currentLevel));
    //     }
    //     else if(currentLevel < 17) {    //Just spawn text sparks for next 3 levels (15-17)
    //         //levelManager.setToWinOnTimeOut();
    //         SetupSparkPool(true, "text");
    //         SetupGnatPool(false, "none", 0);
    //     }
    //     else if(currentLevel < 18) {  //Spawn both text types for 3 levels (16-18)
    //         SetupSparkPool(true, "text");
    //         SetupGnatPool(true, "text", (currentLevel));
    //     }
    //     else {
    //         SetupRandomGameSpawn();
    //     }
    // }

    void GetLevelSpawns() {
        switch (currentLevel) {
            case 1:
                levelManager.SetToWinOnTimeOut();
                SetupSparkPool(false, "none");
                SetupGnatPool(true, "normal", 14);
                return;
            case 2:
                levelManager.SetToWinOnTimeOut();
                SetupSparkPool(false, "none");
                SetupGnatPool(true, "normal", 16);
                return;
            case 3:
                levelManager.SetToWinOnTimeOut();
                SetupSparkPool(false, "none");
                SetupGnatPool(true, "normal", 18);
                return;
            case 4:
                SetupSparkPool(true, "normal");
                SetupGnatPool(false, "none", 0);
                return;
            case 5:
                SetupSparkPool(true, "normal");
                SetupGnatPool(false, "none", 0);
                return;
            case 6:
                SetupSparkPool(true, "normal");
                SetupGnatPool(false, "none", 0);
                return;
            case 7:
                SetupSparkPool(true, "normal");
                SetupGnatPool(true, "normal", 10);
                return;
            case 8:
                SetupSparkPool(true, "normal");
                SetupGnatPool(true, "normal`", 12);
                return;
            case 9:
                SetupSparkPool(true, "normal");
                SetupGnatPool(true, "normal", 14);
                return;
            case 10:
                SetupSparkPool(true, "normal");
                SetupGnatPool(true, "normal", 16);
                return;
            default:
                SetupSparkPool(true, "normal");
                SetupGnatPool(true, "normal", 16);
                return;
        }
    }

    void SetupFastGnattSpawn() {
        levelManager.SetToWinOnTimeOut();
        SetupSparkPool(false, "normal");
        SetupGnatPool(true, "fast", 15);
    }

    void SetupTankGnattSpawn() {
        levelManager.SetToWinOnTimeOut();
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
        gnatPool = new Queue<GameObject>();
        //for(int i=0; i<(levelManager.maxTime/gnatSpawnTime); i++) {
        for(int i=0; i<numOfGnats; i++) {
            GameObject gnat = null;
            if(gnatType == "normal") {
                gnat = Instantiate(gnat_normal, new Vector2(0, 0), Quaternion.identity);
                gnatPool.Enqueue(gnat);
            }
            else if(gnatType == "text") {
                gnat = Instantiate(gnat_text, new Vector2(0, 0), Quaternion.identity);
                gnat.GetComponent<Gnatt_Text>().SetText(badWords[Random.Range(0, badWords.Count)]);
                gnatPool.Enqueue(gnat);
            }

            if(gnat != null) {
                gnat.GetComponent<Gnatt>().SetSpeed(gnatSpeed);
                gnat.SetActive(false);
            }
        }
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
        sparkPoolQu = new Queue<GameObject>();
        for(int i=0; i<20; i++) {
            GameObject spark = null;
            if(sparkType == "normal") {
                spark = Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity);
                sparkPoolQu.Enqueue(spark);
            }
            else if(sparkType == "text") {
                spark = Instantiate(spark_text, new Vector2(0, 0), Quaternion.identity);
                sparkPoolQu.Enqueue(spark);
                spark.GetComponent<Spark_Text>().SetText(goodWords[Random.Range(0, goodWords.Count)]);
            }

            if(spark != null) {
                spark.SetActive(false);
            }
        }
    }

    float PickGnatSpeed() {
        return 0.3f;
        // switch (currentLevel) {
        //     case 0:
        //         return 0f;
        //     case 1:
        //         return 0.2f;
        //     case 2:
        //         return 0.4f;
        //     case 6:
        //         return 0.2f;
        //     case 7:
        //         return 0.3f;
        //     case 8:
        //         return 0.4f;
        //     case 9:
        //         return 0.5f;
        //     case 10:
        //         return 0.6f;
        //     case 11:
        //         return 0.4f;
        //     case 12:
        //         return 0.5f;
        //     case 13:
        //         return 0.6f;
        //     default:
        //         return currentLevel/10f <= 0.7f ? currentLevel/10f : 0.7f;
        // }
    }
}
