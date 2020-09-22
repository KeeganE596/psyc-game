using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> _levelSets;

    private int _currentLevelSet = 0;
    private LevelSettings[] _levelSettings;

    private void Start() 
    {
        ToSavedLevelSet();
    }

    private void CheckLevelUnlock() 
    {
        int currentLevelUnlocked = PlayerPrefs.GetInt("LevelUnlocked");
        Button[] buttons = _levelSets[_currentLevelSet].transform.GetChild(0).GetChild(0).GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            
            if (i == 0 && _currentLevelSet == 0)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Start";
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = 50;
            }
            else
               buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();


            if ((i + (20 * _currentLevelSet)) > currentLevelUnlocked)
            {
                buttons[i].enabled = false;
                buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }

        //for(int i=0; i<_levelSet_1_gameButtons.Count; i++) 
        //{
        //    GameObject b = _levelSet_1_gameButtons[i];
        //    if(i > currentLevelUnlocked) //lock levels above current unlocked
        //    { 
        //        b.GetComponent<Button>().enabled = false;
        //        b.transform.GetChild(0).GetComponent<Image>().enabled = false;
        //    }
        //}
    }

    public void ToMenu() 
    {
        SceneLoadManager.ToMenu();
    }

    public void NextLevelSet()
    {
        _levelSets[_currentLevelSet].SetActive(false);
        _currentLevelSet++;
        _levelSets[_currentLevelSet].SetActive(true);
        CheckLevelUnlock();
        GetAllCurrentLevelSettings();
    }

    public void PreviousLevelSet()
    {
        _levelSets[_currentLevelSet].SetActive(false);
        _currentLevelSet--;
        _levelSets[_currentLevelSet].SetActive(true);
        CheckLevelUnlock();
        GetAllCurrentLevelSettings();
    }

    public void ToSavedLevelSet()
    {
        foreach(GameObject ls in _levelSets)
        {
            ls.SetActive(false);
        }

        _currentLevelSet = GameManagerStatic.GetCurrentLevelSetNumber();
        _levelSets[_currentLevelSet].SetActive(true);
        CheckLevelUnlock();
        GetAllCurrentLevelSettings();
    }

    private void GetAllCurrentLevelSettings()
    {
        _levelSettings = _levelSets[_currentLevelSet].GetComponentsInChildren<LevelSettings>();
        GameManagerStatic.SetLevels(_levelSettings);
        //PrintLevelAllSettings();
    }

    private void PrintLevelAllSettings()
    {
        Debug.Log(_levelSettings.Length);
        for (int i=0; i<_levelSettings.Length; i++)
        {
            Debug.Log(_levelSettings[i].ToString());
        }
    }
}
