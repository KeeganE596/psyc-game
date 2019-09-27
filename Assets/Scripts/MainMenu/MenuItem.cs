using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public GameObject forScript;
    MainMenu_Animations mmScript;

    // Start is called before the first frame update
    void Start()
    {
        mmScript = forScript.GetComponent<MainMenu_Animations>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D() {
        mmScript.menuItemDetected(gameObject.name);
    }

    public void OnTriggerExit2D() {
        mmScript.menuItemDetected("exit");
    }
}
