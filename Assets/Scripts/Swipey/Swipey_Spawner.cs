using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swipey_Spawner: Spawns in sparks and gnats at the appropriate times.
//Stores sparks and Gnats in object pools to mitigate need to instantiate/destroy objects all the time
//Ability to have different spark and gnat types
public class Swipey_Spawner : MonoBehaviour
{
    public static Swipey_Spawner Instance { get; private set; }

    [SerializeField] private Camera mainCam;

    //Sparx
    Queue<GameObject> sparkPool;
    [SerializeField] private GameObject spark_1;
    [SerializeField] private GameObject spark_text;

    //Gnats
    Queue<GameObject> gnatPool;
    [SerializeField] private GameObject gnat_normal;
    [SerializeField] private GameObject gnat_text;

    //Spawn timings
    float nextSparkSpawnTime;
    float sparkSpawnOffset;
    float nextGnatSpawnTime;
    float gnatSpawnOffset;

    bool spawnSparks;
    bool spawnGnats;

    [SerializeField] private List<string> goodWords;
    [SerializeField] private List<string> badWords;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying Spawner duplicate");
            Destroy(gameObject); //Don't allow two LevelManagers
        }
    }

    void Start() 
    {
        if (GameManagerStatic.GetPlayingRandomGame())
            SetupRandomGameSpawn();
        else 
            GetLevelSpawns();
    }

    void Update() 
    {
        //if(LevelManager.Instance.GetIfGameIsPlaying()) {
            if(spawnSparks) 
            {
                if(Time.time >= nextSparkSpawnTime) 
                {
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
        //}
    }


    void spawnSpark() {
        if(sparkPool.Count == 0) {
            Debug.Log("No Sparx to spawn");
            return;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = mainCam.ScreenToWorldPoint(new Vector2(Random.Range(100, Screen.width-100), Random.Range(75, Screen.height-120)));

        //check position isnt too close to middle
        if((spawnPos.x < -2.5 || spawnPos.x > 2.5) && (spawnPos.y < -2.5 || spawnPos.y > 2.5)) {
            GameObject sp = sparkPool.Dequeue();
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

        GameObject gnat = gnatPool.Dequeue();
        int side = Random.Range(0, 2);  //pick which side of screen to spawn on
        Vector2 spawnPos;
        float y = Random.Range(0, Screen.height);
        if (side == 0) { //if left side of screen
            spawnPos = mainCam.ScreenToWorldPoint(new Vector2(-10, y));
        }
        else {  //if right side of screen
            spawnPos = mainCam.ScreenToWorldPoint(new Vector2(Screen.width+10, y));
            gnat.GetComponent<Gnat>().FlipSprite();
        }

        gnat.transform.position = spawnPos;
        gnat.SetActive(true);
        gnat.GetComponent<Gnat>().ChooseSprite();
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
        LevelSettings levelSettings = GameManagerStatic.GetCurrentLevelSettings();
        SetupSparkPool(levelSettings.GetIfHasSparks(), levelSettings.GetSparkType());
        SetupGnatPool(levelSettings.GetIfHasGnats(), levelSettings.GetGnatType(), levelSettings.GetNumberOfGnats());
        //Debug.Log(levelSettings.ToString());

        //switch (GameManagerStatic.GetCurrentLevelNumber()) {
        //    case 1:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "normal", 14);
        //        return;
        //    case 2:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "normal", 16);
        //        return;
        //    case 3:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "normal", 18);
        //        return;
        //    case 4:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    case 5:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    case 6:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    case 7:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(true, "normal", 10);
        //        return;
        //    case 8:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(true, "normal`", 12);
        //        return;
        //    case 9:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(true, "normal", 14);
        //        return;
        //    case 10:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "text", 16);
        //        return;
        //    case 11:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "text", 16);
        //        return;
        //    case 12:
        //        SetupSparkPool(false, "none");
        //        SetupGnatPool(true, "text", 16);
        //        return;
        //    case 13:
        //        SetupSparkPool(true, "text");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    case 14:
        //        SetupSparkPool(true, "text");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    case 15:
        //        SetupSparkPool(true, "text");
        //        SetupGnatPool(false, "none", 0);
        //        return;
        //    default:
        //        SetupSparkPool(true, "normal");
        //        SetupGnatPool(true, "normal", 16);
        //        return;
        //}
    }

    void SetupGnatPool(bool spawningGnats, string gnatType, int numOfGnats) {
        if(!spawningGnats || gnatType == "none") {
            //Do not spawn gnats
            spawnGnats = false;
            gnatSpawnOffset = Mathf.Infinity;
            return;
        }

        if(gnatType == "random")
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
                gnatType = "normal";
            else
                gnatType = "text";
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
            LevelManager.Instance.SetToWinOnTimeOut();
            return;
        }

        if (sparkType == "random")
        {
            int rand = Random.Range(0, 2);
            if (rand == 1)
                sparkType = "normal";
            else
                sparkType = "text";
        }

        //Sparks spawn intialise settings
        spawnSparks = true;
        sparkSpawnOffset = LevelManager.Instance.maxTime/15;
        nextSparkSpawnTime = sparkSpawnOffset;
        
        //Instantiate sparks to fill object pool with appropriate sparks for the level
        sparkPool = new Queue<GameObject>();
        for(int i=0; i<20; i++) {
            GameObject spark = null;
            if(sparkType == "normal") {
                spark = Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity);
                sparkPool.Enqueue(spark);
            }
            else if(sparkType == "text") {
                spark = Instantiate(spark_text, new Vector2(0, 0), Quaternion.identity);
                sparkPool.Enqueue(spark);
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

    public void AddGnatToQueue(GameObject gnat)
    {
        gnatPool.Enqueue(gnat);
    }

    public void AddSparkToQueue(GameObject spark)
    {
        sparkPool.Enqueue(spark);
    }
}
