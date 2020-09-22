using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionGameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Animator gameTextAnimator;

    [SerializeField] private GameObject pausePanel;

    Queue<GameObject> sparkPoolQu;
    [SerializeField] private GameObject spark_1;
    Queue<GameObject> gnatPool;
    [SerializeField] private GameObject gnat_normal;
    float nextSparkSpawnTime;
    float sparkSpawnOffset;
    float nextGnatSpawnTime;
    float gnatSpawnOffset;
    bool spawnSparks = false;
    bool spawnGnats = false;

    float maxTime = 15f;
    float timeRemaining;
    bool playing = false;
    bool playingSecondHalf = false;


    // Start is called before the first frame update
    void Start() {
        timeRemaining = maxTime;

        SetupGnatPool(6);
        SetupSparkPool();
    }

    // Update is called once per frame
    void Update() {
        if(playing) { //was getifplaying
            //Slider stuff
            timeSlider.value = timeRemaining/maxTime;
            if(timeRemaining <= 0) {
                timeRemaining = 0;
                playing = false;
                GameWon();
                DestroySparx();
                gameTextAnimator.SetTrigger("nextText");
            }
            if(timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }

            if(timeRemaining > 7.5f) {
                spawnGnats = true;
                spawnSparks = false;
            }
            else if (playingSecondHalf) {
                spawnGnats = false;
                spawnSparks = true;
            }
            else if(!playingSecondHalf) {
                playing = false;
                DestroyGnats();
                gameTextAnimator.SetTrigger("nextText");
                StartCoroutine("StartTimerSecondHalf");
            }

            //Spawning stuff
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

    public void StartPlaying() {
        StartCoroutine("StartTimer");
    }

    IEnumerator StartTimer() {
        yield return new WaitForSeconds(6f);
        playing = true;
    }

    IEnumerator StartTimerSecondHalf() {
        yield return new WaitForSeconds(6f);
        playing = true;
        playingSecondHalf = true;
    }

    void DestroyGnats() {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Gnat")) {
            g.GetComponent<Gnat>().Despawn();
        }
    }

    void DestroySparx() {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Spark")) {
            g.GetComponent<Spark>().Activate();
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

    void SetupSparkPool() {
        //Sparks spawn intialise settings
        sparkSpawnOffset = 1; //LevelManager.Instance.maxTime/15;
        nextSparkSpawnTime = sparkSpawnOffset;
        
        //Instantiate sparks to fill object pool with appropriate sparks for the level
        sparkPoolQu = new Queue<GameObject>();
        for(int i=0; i<20; i++) {
            GameObject spark = Instantiate(spark_1, new Vector2(0, 0), Quaternion.identity);
            sparkPoolQu.Enqueue(spark);

            if(spark != null) {
                spark.SetActive(false);
            }
        }
    }

    void SetupGnatPool(int numOfGnats) {
        //Gnats spawn intialise settings
        gnatSpawnOffset = 7.5f/numOfGnats; // LevelManager.Instance.maxTime/numOfGnats;
        nextGnatSpawnTime = gnatSpawnOffset;
        float gnatSpeed = 0.2f;

        //Instantiate gnats to fill object pool with appropriate gnat for the level
        gnatPool = new Queue<GameObject>();
        for(int i=0; i<numOfGnats; i++) {
            GameObject gnat = Instantiate(gnat_normal, new Vector2(0, 0), Quaternion.identity);
            gnatPool.Enqueue(gnat);

            if(gnat != null) {
                gnat.GetComponent<Gnat>().SetSpeed(gnatSpeed);
                gnat.SetActive(false);
            }
        }
    }

    public void ToMainMenu() {
        PauseGame(false);
        SceneLoadManager.ToMenu();
    }

    public void ToMap() {
        PauseGame(false);
        SceneLoadManager.ToMap();
    }

    public void NextLevel() {
        GameManagerStatic.PickGame(1);
    }

    public void RestartGame() {
        GameManagerStatic.PlaySameGame();
    }

    void GameWon() {
        SaveManager.AddToSparxScore();

        int lvl = PlayerPrefs.GetInt("LevelUnlocked");
        if(GameManagerStatic.GetCurrentLevelNumber() == lvl) {
            SaveManager.SaveLevelUnlocked(lvl+1);
        }
    }

    public void PauseGame(bool paused) {
        if(paused) {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
