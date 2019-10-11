using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_TouchDetection : MonoBehaviour
{
    public GameObject expandBlob;
    public GameObject outerCircle;

    bool held;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            expandBlob.transform.localScale += new Vector3(0.013f, 0.013f, 0);
        }
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (expandBlob.transform.localScale.x >= 1.5 && expandBlob.transform.localScale.x <= 1.65 && held)
            {
                expandBlob.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            }
        }
        else
        {
            if (expandBlob.transform.localScale.x >= 0.2)
            {
                expandBlob.transform.localScale = new Vector3(0.1f, 0.1f, 0);
            }

        }
    }
}

//>1.5 <1.65