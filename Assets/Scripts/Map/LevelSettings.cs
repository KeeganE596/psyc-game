using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private int _levelNumber = 0;
    [SerializeField] private bool _hasSparks = false;
    [SerializeField] private bool _hasGnats = false;
    [SerializeField] private string _sparkType = "none";
    [SerializeField] private string _gnatType = "none";
    [SerializeField] private int _numberOfGnats = 0;

    public LevelSettings(int levelNumber, bool hasSparks, bool hasGnats, string sparkType, string gnatType, int numberOfGnats)
    {
        _levelNumber = levelNumber;
        _hasSparks = hasSparks;
        _hasGnats = hasGnats;
        _sparkType = sparkType;
        _gnatType = gnatType;
        _numberOfGnats = numberOfGnats;
    }

    public int GetLevelNumber() {
        return _levelNumber;
    }

    public bool GetIfHasSparks()
    {
        return _hasSparks;
    }

    public bool GetIfHasGnats()
    {
        return _hasGnats;
    }

    public string GetSparkType()
    {
        return _sparkType;
    }

    public string GetGnatType()
    {
        return _gnatType;
    }

    public int GetNumberOfGnats()
    {
        return _numberOfGnats;
    }

    public override string ToString()
    {
        return _levelNumber + ", " + _hasSparks + ", " + _hasGnats + ", " + _sparkType + ", " + _gnatType + ", " + _numberOfGnats;
    }
}
