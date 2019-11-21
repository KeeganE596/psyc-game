using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection_ThoughtsController : MonoBehaviour
{
    Vector3 worldScale;
    List<string> positiveThoughts;
    List<string> negativeThoughts;
    List<GameObject> thoughts;

    public GameObject thoughtObj;

    float SCREEN_WIDTH = Screen.width;
    float SCREEN_HEIGHT = Screen.height;
    float xPos;
    float yPos = -2;
    float xShift;
    float thoughtSpeed;
    bool isClicked;

    // Start is called before the first frame update
    void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        positiveThoughts = new List<string>();
        negativeThoughts = new List<string>();
        thoughts = new List<GameObject>();

        positiveThoughts.Add("positive1"); positiveThoughts.Add("positive2"); positiveThoughts.Add("positive3");

        negativeThoughts.Add("negative1"); negativeThoughts.Add("negative2"); negativeThoughts.Add("negative3");

        xShift = (worldScale.x*2)/4;
        xPos = (0-worldScale.x) - xShift*2;

        thoughtSpeed = 0.05f;

        isClicked = false;

        spawnThoughts();
    }

    // Update is called once per frame
    void Update() {
        if(!isClicked) {
            foreach(GameObject t in thoughts) {
                t.transform.position = new Vector2(t.transform.position.x + thoughtSpeed, yPos);
                if(t.transform.position.x > (worldScale.x + 0.5f)) {
                    t.SetActive(false);
                    t.transform.position = new Vector2((0-worldScale.x) - xShift*2, yPos);
                }
                if(t.transform.position.x > (0-worldScale.x - 0.5f)) {
                    t.SetActive(true);
                }
            }
        }
    }

    public void spawnThoughts() {
        while(positiveThoughts.Count > 0 || negativeThoughts.Count > 0) {
            int thoughtType = Random.Range(0, 2);

            if((thoughtType == 0 && positiveThoughts.Count > 0) || (thoughtType == 1 && negativeThoughts.Count > 0)) {  //positve thought
                GameObject thought = Instantiate(thoughtObj, new Vector2(xPos, yPos), Quaternion.identity);
                
                if(thoughtType == 0) {
                    int thoughtIndex = Random.Range(0, positiveThoughts.Count);
                    thought.GetComponent<Inspection_ThoughtObject>().setThought(positiveThoughts[thoughtIndex], true);
                    positiveThoughts.RemoveAt(thoughtIndex);
                }
                if(thoughtType == 1) {
                    int thoughtIndex = Random.Range(0, negativeThoughts.Count);
                    thought.GetComponent<Inspection_ThoughtObject>().setThought(negativeThoughts[thoughtIndex], false);
                    negativeThoughts.RemoveAt(thoughtIndex);
                }

                xPos += xShift;
                thoughts.Add(thought);
            }
        }
    }

    public void RemoveThought(GameObject thought) {
        thought.SetActive(false);
        thoughts.Remove(thought);
    }

    public void clickedThought() {
        isClicked = !isClicked;
    }

    public void clickedThought(bool b) {
        isClicked = b;
    }
}
