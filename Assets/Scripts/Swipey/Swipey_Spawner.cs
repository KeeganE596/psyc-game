using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_Spawner: Spawns in sparks and gnats at the appropriate times.
//Stores sparks and Gnats in object pools to mitigate need to instantiate/destroy objects all the time
//Ability to have different spark and gnat types
public class Swipey_Spawner : MonoBehaviour
{    
    [SerializeField] private Camera mainCam;

    //Sparx
    Queue<GameObject> sparkPoolQu;
    [SerializeField] private GameObject spark_1;
    [SerializeField] private GameObject spark_text;

    //Gnats
    Queue<GameObject> gnatPool;
    [SerializeField] private GameObject gnat_normal;
    [SerializeField] private GameObject gnat_text;

    bool playing;

    //Spawn timings
    float nextSparkSpawnTime;
    float sparkSpawnOffset;
    float nextGnatSpawnTime;
    float gnatSpawnOffset;

    int currentLevel;

    bool spawnSparks;
    bool spawnGnats;

    List<string> goodWords;
    List<string> badWords;

    void Awake() {
    }
    
    // Start is called before the first frame update
    void Start() {
        playing = LevelManager.Instance.GetIfGameIsPlaying();
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

        if(GameManagerStatic.GetPlayingRandomGame()) {
            SetupRandomGameSpawn();
        }
        else {
            GetLevelSpawns();
        }
    }

    // Update is called once per frame
    void Update() {
        //Check player isn't on instruction screen
        if(LevelManager.Instance.GetIfGameIsPlaying()) {
            if(spawnSparks) {
                if(Time.time >= nextSparkSpawnTime) {
                    spawnSpark();
                    nextSparkSpawnTime = Time.time + sparkSpawnOffset;
                }
            }
            if(spawnGnats) {
                if(Time.time >= nextGnatSpawnTime) {
                    spawnGnat();
                    nextGnatSpawnTime = Time.time + gnatSpawnOffset;
                }
            }
        }
    }


    void spawnSpark() {
        if(sparkPoolQu.Count == 0) {
            return;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = mainCam.ScreenToWorldPoint(new Vector2(Random.Range(100, Screen.width-100), Random.Range(75, Screen.height-120)));

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
            spawnPos = mainCam.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {  //if right side of screen
            float y = Random.Range(0, Screen.height);
            spawnPos = mainCam.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
            gn.GetComponent<Gnat>().FlipSprite();
        }

        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gn.GetComponent<Gnat>().ChooseSprite();
    }

    void ChooseRandomGameType() {
             SetupRandomGameSpawn();
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

    void GetLevelSpawns() {
        switch (currentLevel) {
            case 1:
                LevelManager.Instance.SetToWinOnTimeOut();
                SetupSparkPool(false, "none");
                SetupGnatPool(true, "normal", 14);
                return;
            case 2:
                LevelManager.Instance.SetToWinOnTimeOut();
                SetupSparkPool(false, "none");
                SetupGnatPool(true, "normal", 16);
                return;
            case 3:
                LevelManager.Instance.SetToWinOnTimeOut();
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

    void SetupGnatPool(bool spawningGnats, string gnatType, int numOfGnats) {
        if(!spawningGnats || gnatType == "none") {
            //Do not spawn gnats
            spawnGnats = false;
            gnatSpawnOffset = Mathf.Infinity;
            return;
        }

        //Gnats spawn intialise settings
        spawnGnats = true;
        gnatSpawnOffset = LevelManager.Instance.maxTime/numOfGnats;
        nextGnatSpawnTime = gnatSpawnOffset;
        float gnatSpeed = PickGnatSpeed();

        //Instantiate gnats to fill object pool with appropriate gnat for the level
        gnatPool = new Queue<GameObject>();
        for(int i=0; i<numOfGnats; i++) {
            GameObject gnat = null;
            if(gnatType == "normal") {
                gnat = Instantiate(gnat_normal, new Vector2(0, 0), Quaternion.identity);
                gnatPool.Enqueue(gnat);
            }
            else if(gnatType == "text") {
                gnat = Instantiate(gnat_text, new Vector2(0, 0), Quaternion.identity);
                gnat.GetComponent<Gnat_Text>().SetText(badWords[Random.Range(0, badWords.Count)]);
                gnatPool.Enqueue(gnat);
            }

            if(gnat != null) {
                gnat.GetComponent<Gnat>().SetSpeed(gnatSpeed);
                gnat.SetActive(false);
            }
        }
    }
    
    void SetupSparkPool(bool spawningSparks, string sparkType) {
        if(!spawningSparks || sparkType == "none") {
            //Do not spawn sparks
            spawnSparks = false;
            sparkSpawnOffset = Mathf.Infinity;
            return;
        }

        //Sparks spawn intialise settings
        spawnSparks = true;
        sparkSpawnOffset = LevelManager.Instance.maxTime/15;
        nextSparkSpawnTime = sparkSpawnOffset;
        
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
        return 0.2f;
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
