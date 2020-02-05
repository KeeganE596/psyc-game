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
    float yPos = 2;
    float xShift;
    float thoughtSpeed;
    bool isClicked;

    // Start is called before the first frame update
    void Start() {
        worldScale = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        positiveThoughts = new List<string>();
        negativeThoughts = new List<string>();
        thoughts = new List<GameObject>();

        positiveThoughts.Add("I can give this a try"); positiveThoughts.Add("I have done it before"); positiveThoughts.Add("Thoughts are just thoughts");
        positiveThoughts.Add("I will take it slow"); positiveThoughts.Add("It is normal to feel upset sometimes"); positiveThoughts.Add("I see problems as challenges");
        positiveThoughts.Add("It happened, what will I do next time?"); positiveThoughts.Add("I am happy with my self"); positiveThoughts.Add("It's okay, this will pass");
        positiveThoughts.Add("I am open to new things"); positiveThoughts.Add("Emotions are harmless, even though they can be unpleasant"); positiveThoughts.Add("I trust myself");
        positiveThoughts.Add("Thoughts are thoughts, they are not true or false"); positiveThoughts.Add("I can ask for help if I need to");
        positiveThoughts.Add("I will do the best I can"); positiveThoughts.Add("I will try my best"); positiveThoughts.Add("I can not predict the future");
        positiveThoughts.Add("I have the skills to cope with setbacks");

        negativeThoughts.Add("This happens to me <b>all</b> the time"); negativeThoughts.Add("I can never do anything right"); negativeThoughts.Add("I should have done this");
        negativeThoughts.Add("<b>I</b> could have done that"); negativeThoughts.Add("I'm not as good as others"); negativeThoughts.Add("What if something goes wrong?");
        negativeThoughts.Add("What are they thinking about me?"); negativeThoughts.Add("I need to look a certain way"); negativeThoughts.Add("I need to fit in");
        negativeThoughts.Add("I have to be liked by everyone"); negativeThoughts.Add("What if I fail my test?"); 

        xShift = (worldScale.x*2)/4;
        xPos = (0-worldScale.x) - xShift*2;

        thoughtSpeed = 0.04f;

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
        while(thoughts.Count < 6) {
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

    public bool CheckIfWin() {
        foreach(GameObject t in thoughts) {
            if(!t.GetComponent<Inspection_ThoughtObject>().GetIfChecked()) {
                return false;
            }
        }
        return true;
    }
}
