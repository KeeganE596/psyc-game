using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupLevelSet : MonoBehaviour
{
    [SerializeField] private Color _levelSetColor;
    [SerializeField] private int _levelSetNumber;

    void Start()
    {
        SetColors();
    }

    private void SetColors()
    {
        Transform gameButtons = transform.GetChild(0).GetChild(0);
        foreach(Transform b in gameButtons)
        {
            if (b.gameObject == this)
                continue;
            b.GetChild(0).GetComponent<Image>().color = _levelSetColor;
            b.GetComponent<MapButton>().SetLevelSetNumber(_levelSetNumber);
        }

        GetComponentInChildren<Scrollbar>().transform.GetChild(0).GetChild(0).GetComponent<Image>().color = _levelSetColor;

    }
}
