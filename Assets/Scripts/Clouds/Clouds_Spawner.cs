using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds_Spawner : MonoBehaviour
{
    LevelManager levelManager;

    List<GameObject> gnatPool;
    public GameObject gnat_1;
    public GameObject gnat_2;
    bool spawnGnats;
    float gnatTimer;
    float gnatSpawnTime;
    int gnatIndex;

    List<GameObject> sparkPool;
    public GameObject spark_1;
    public GameObject spark_2;
    bool spawnSparks;
    float sparkTimer;
    float sparkSpawnTime;
    int sparkIndex;


    void Awake() {
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    // Start is called before the first frame update
    void Start() {
        SetupSparkPool(true, "normal");
        SetupGnatPool(false, "none", (10));
    }

    // Update is called once per frame
    void Update() {
        if(LevelManager.Instance.GetIfGameIsPlaying()) {
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

    void spawnGnat() {
        if(gnatIndex >= gnatPool.Count) {
            gnatIndex = 0;
        }
        
        if(gnatPool[gnatIndex].activeSelf) {
            gnatIndex++;
            spawnGnat();
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
            gn.GetComponent<Gnat>().FlipSprite();
        }

        gn.transform.position = spawnPos;
        gn.SetActive(true);
        gnatIndex++;
    }

    void spawnSpark() {
        if(sparkIndex >= sparkPool.Count) {
            sparkIndex = 0;
        }
        //Get random position to spawn spark at
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(100, Screen.width-100), Random.Range(75, Screen.height-120)));
    
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

    void SetupGnatPool(bool spawningGnats, string gnatType, int numOfGnats) {
        
        if(!spawningGnats || gnatType == "none") {
            //Do not spawn gnats
            spawnGnats = false;
            gnatTimer = 0;
            gnatSpawnTime = Mathf.Infinity;
            return;
        }

        //Gnats spawn intialise settings
        spawnGnats = true;
        gnatTimer = 0.1f;
        //gnatSpawnTime = (1.5f - ((currentLevel+1)*0.1f)) >= 0.35f ? 1.5f - ((currentLevel+1)*0.1f) : 0.35f;  //scale gnat spawn speed depending on level number
        gnatSpawnTime = LevelManager.Instance.maxTime/numOfGnats;
        float gnatSpeed = 0.3f;//PickGnatSpeed();

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
            // else if(gnatType == "text") {
            //     gnatPool.Add(Instantiate(gnat_text, new Vector2(0, 0), Quaternion.identity)); 
            //     gnatPool[i].GetComponent<Gnat_Text>().SetText(badWords[Random.Range(0, badWords.Count)]);
                
            // }
            gnatPool[i].GetComponent<Gnat>().SetSpeed(gnatSpeed);
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
        sparkSpawnTime = LevelManager.Instance.maxTime/15;
        
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
            // else if(sparkType == "text") {
            //     sparkPool.Add(Instantiate(spark_text, new Vector2(0, 0), Quaternion.identity));
            //     sparkPool[i].GetComponent<Spark_Text>().SetText(goodWords[Random.Range(0, goodWords.Count)]);
            // }

            sparkPool[i].SetActive(false);
        }
        sparkIndex = 0;
    }
}
