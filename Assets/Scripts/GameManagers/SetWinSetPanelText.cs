using UnityEngine;

public class SetWinSetPanelText : MonoBehaviour
{
    void Start()
    {
        int setNum = GameManagerStatic.GetCurrentLevelSetNumber();
        transform.GetChild(setNum).gameObject.SetActive(true);
    }
}
