using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    Color currentBgColor;
    GameObject bgColorObj;
    Color lightBlue = new Color32(166, 203, 209, 255);
    Color lightPink = new Color32(214, 161, 198, 255);
    Color lightGreen = new Color32(179, 217, 158, 255);
    Color lightOrange = new Color32(231, 192, 148, 255);

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        currentBgColor = lightBlue;

        GameObject camParent = GameObject.FindWithTag("Cameras");
        camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = currentBgColor;
    }

    public void SetColor(GameObject colObj) {
        if(bgColorObj != null) {
            bgColorObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = new Color32(0, 0, 0, 50);
        }

        currentBgColor = colObj.transform.GetChild(1).gameObject.GetComponent<SVGImage>().color;
        bgColorObj = colObj;

        colObj.transform.GetChild(0).gameObject.GetComponent<SVGImage>().color = Color.white;

        setCameraBackground();
    }

    public void setCameraBackground() {
        GameObject camParent = GameObject.FindWithTag("Cameras");
        camParent.transform.GetChild(0).gameObject.GetComponent<Camera>().backgroundColor = currentBgColor;
    }

    public Color32 GetColor() {
        return currentBgColor;
    }
}
