using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ObjectSpawner: REDUNDANT spawner script for swipey game.
//SPWIPEY_SPAWNER SCRIPT NOW USED
public class Object_Spawner : MonoBehaviour
{
    bool startGame;
    public GameObject sparkPrefab;
    public GameObject gnattsPrefab;

    GameObject left;
    GameObject right;

    public Collider2D[] colliders;
    public float radius;
    public LayerMask mask;

    public float sparkRespawnTime = 1.0f;
    public float gnattRespawnTime = 1.0f;

    public bool spawningGnatt = true;
    public bool spawningSpark = true;

    private void Start() {
        CreateGnattSpawners();

        StartCoroutine(sparkSpawn());
        StartCoroutine(gnattSpawn());   
    }
    private void Awake()
    {
        left = new GameObject("Left");
        left.layer = LayerMask.NameToLayer("Spawn Colliders");
        right = new GameObject("Right");
        right.layer = LayerMask.NameToLayer("Spawn Colliders");
        //startGame = false;
    }

    void startup() {
        CreateGnattSpawners();

        StartCoroutine(sparkSpawn());
        StartCoroutine(gnattSpawn());

        left = new GameObject("Left");
        left.layer = LayerMask.NameToLayer("Spawn Colliders");
        right = new GameObject("Right");
        right.layer = LayerMask.NameToLayer("Spawn Colliders");
    }

    private void CreateGnattSpawners() {

        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        // Create Left Colliders
        BoxCollider2D collider = left.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;
        
        left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);
    

        // Create Right Colliders
        collider = right.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;

        right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);
    }

    private void spawnGnatt()
    {
        BoxCollider2D lB = left.GetComponent<BoxCollider2D>();
        BoxCollider2D rB = right.GetComponent<BoxCollider2D>();
        GameObject n1 = Instantiate(gnattsPrefab) as GameObject;
        GameObject n2 = Instantiate(gnattsPrefab) as GameObject;

        //find Left Gnatt spawner dimensions.
        float Lx = lB.transform.position.x;
        float Ly = lB.transform.position.y;
        Vector2 lColliderSize = lB.size;
        float Lwidth = lColliderSize.x;
        float Lheight = lColliderSize.y;
        
        //find Right Gnatt spawner dimensions.
        float Rx = rB.transform.position.x;
        float Ry = rB.transform.position.y;
        Vector2 rColliderSize = rB.size;
        float Rwidth = rColliderSize.x;
        float Rheight = rColliderSize.y;

            n1.transform.position = (new Vector2(Random.Range(Lx, Lx + Lwidth), Random.Range(Ly, Ly + Lheight)));
            n2.transform.position = (new Vector2(Random.Range(Rx, Rx + Rwidth), Random.Range(Ry, Ry + Rheight)));
    }
    IEnumerator gnattSpawn()
    {
        while (spawningGnatt == true)
        {
            yield return new WaitForSeconds(gnattRespawnTime);
            spawnGnatt();
        }
    }

    private void spawnSpark() {

        Vector2 spawnPos = new Vector2(0,0);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere) {
            //random X and Y position based on camera screen size.
            float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            spawnPos = new Vector2(spawnX, spawnY);

            //Passes randomly generated spawn position into a function that checks if there are any overlaps of colliders
            canSpawnHere = PreventSpawnOverLap(spawnPos);
            if (canSpawnHere == true) {
                break;
            }
            safetyNet++;
            if (safetyNet > 50) {
                break;
            }
        }

            GameObject s = Instantiate(sparkPrefab, spawnPos, Quaternion.identity) as GameObject;
    }
    IEnumerator sparkSpawn() {
        while (spawningSpark == true) {
            yield return new WaitForSeconds(sparkRespawnTime);
            spawnSpark();
        }
    }



    bool PreventSpawnOverLap(Vector2 spawnPos) {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, mask);

        for (int i = 0; i < colliders.Length; i++) {
            Vector2 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent) {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent) {
                    return false;
                }
            }
        }
        return true;
    }
}
