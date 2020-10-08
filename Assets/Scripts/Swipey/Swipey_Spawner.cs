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
    [SerializeField] private GameObject spark_2;
    [SerializeField] private GameObject spark_text;

    //Gnats
    Queue<GameObject> gnatPool;
    [SerializeField] private GameObject gnat_normal;
    [SerializeField] private GameObject gnat_text;
    [SerializeField] private GameObject gnat_fast;
    [SerializeField] private GameObject gnat_tank;

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
        //if (GameManagerStatic.GetPlayingRandomGame())
        //    //SetupRandomGameSpawn();
        //else 
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

    //void SetupRandomGameSpawn() {
    //    if(Random.Range(0, 2) == 0) {   //Choose spark type
    //        SetupSparkPool(true, "normal");
    //    }
    //    else {
    //        SetupSparkPool(true, "text");
    //    }
    //    if(Random.Range(0, 2) == 0) {   //Choose gnat type
    //        SetupGnatPool(true, "normal", 18);
    //    }
    //    else {
    //        SetupGnatPool(true, "text", 18);
    //    }
    //}

    void GetLevelSpawns() {
        LevelSettings levelSettings = GameManagerStatic.GetCurrentLevelSettings();
        SetupSparkPool(levelSettings.GetSparkTypes());
        SetupGnatPool(levelSettings.GetGnatTypes(), levelSettings.GetNumberOfGnats());
        //Debug.Log(levelSettings.ToString());
    }

    void SetupGnatPool(List<string> gnatTypes, int numOfGnats)
    {
        if (gnatTypes.Count == 0)   //Don't spawn any gnats
        {
            spawnGnats = false;
            gnatSpawnOffset = Mathf.Infinity;
            return;
        }

        //Gnats spawn intialise settings
        spawnGnats = true;
        gnatSpawnOffset = LevelManager.Instance.maxTime / numOfGnats;
        nextGnatSpawnTime = gnatSpawnOffset;

        string gnatType = gnatType = gnatTypes[0];

        //Instantiate gnats to fill object pool with appropriate gnat for the level
        gnatPool = new Queue<GameObject>();
        for (int i = 0; i < numOfGnats; i++)
        {
            if (gnatTypes.Count > 1)
                gnatType = gnatTypes[Random.Range(0, gnatTypes.Count)];

            GameObject gnat = null;
            if (gnatType == "normal")
            {
                gnat = Instantiate(gnat_normal, new Vector2(0, 0), Quaternion.identity);
            }
            else if (gnatType == "text")
            {
                gnat = Instantiate(gnat_text, new Vector2(0, 0), Quaternion.identity);
                gnat.GetComponent<Gnat_Text>().SetText(badWords[Random.Range(0, badWords.Count)]);
            }
            else if (gnatType == "fast")
            {
                gnat = Instantiate(gnat_fast, new Vector2(0, 0), Quaternion.identity);
            }
            else if (gnatType == "tank")
            {
                gnat = Instantiate(gnat_tank, new Vector2(0, 0), Quaternion.identity);
            }
            gnatPool.Enqueue(gnat);

            if (gnat != null)
                gnat.SetActive(false);
        }
    }

    void SetupSparkPool(List<string> sparkTypes)
    {
        if (sparkTypes.Count == 0)  //Don't spawn any sparks
        {
            spawnSparks = false;
            sparkSpawnOffset = Mathf.Infinity;
            LevelManager.Instance.SetToWinOnTimeOut();
            return;
        }

        //Sparks spawn intialise settings
        spawnSparks = true;
        sparkSpawnOffset = LevelManager.Instance.maxTime / 15;
        nextSparkSpawnTime = sparkSpawnOffset;

        string sparkType = sparkTypes[0];

        //Instantiate sparks to fill object pool with appropriate sparks for the level
        sparkPool = new Queue<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            if (sparkTypes.Count > 1)
                sparkType = sparkTypes[Random.Range(0, sparkTypes.Count)];

            GameObject spark = null;
            if (sparkType == "normal")
            {
                if(Random.Range(0, 2) == 0)
                    spark = Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity);
                else
                    spark = Instantiate(spark_2, new Vector2(0, 0), Quaternion.identity);

            }
            else if (sparkType == "text")
            {
                spark = Instantiate(spark_text, new Vector2(0, 0), Quaternion.identity);
                spark.GetComponent<Spark_Text>().SetText(goodWords[Random.Range(0, goodWords.Count)]);
            }
            sparkPool.Enqueue(spark);

            if (spark != null)
            {
                spark.SetActive(false);
            }
        }
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
