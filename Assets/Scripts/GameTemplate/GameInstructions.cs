using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("readInstructionsWait");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator readInstructionsWait()  {
        Debug.Log("Here");
        yield return new WaitForSeconds(5f);
        Debug.Log("Done");
    }
}
