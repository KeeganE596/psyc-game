using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public void ChooseLevel()
    {
        LevelSettings ls = GetComponent<LevelSettings>();
        GameManagerStatic.PickGame(ls.GetLevelNumber(), ls.GetLevelSetNumber());
    }

    public void ChooseIntro()
    {
        SceneLoadManager.ToIntro();
    }
}
