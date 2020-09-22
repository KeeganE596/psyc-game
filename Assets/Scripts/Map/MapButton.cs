using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    [SerializeField] private int _levelNumber = 0;
    private int _levelSetNumber = 0;

    public void ChooseLevel()
    {
        GameManagerStatic.PickGame(_levelNumber, _levelSetNumber);
    }

    public void ChooseIntro()
    {
        SceneLoadManager.ToIntro();
    }

    public void SetLevelSetNumber(int num)
    {
        _levelSetNumber = num;
    }
}
