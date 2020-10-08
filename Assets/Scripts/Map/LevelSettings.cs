using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private int _levelNumber = 0;

    [Header("Spark Types")]
    [SerializeField] private bool _normalSparx = false;
    [SerializeField] private bool _textSparx = false;

    [Header("Gnat Types")]
    [SerializeField] private int _numberOfGnats = 0;
    [SerializeField] private bool _normalGnat = false;
    [SerializeField] private bool _textGnat = false;
    [SerializeField] private bool _fastGnat = false;
    [SerializeField] private bool _tankGnat = false;

    [Header("Level Instructions")]
    [SerializeField] private bool _hasInstructions = false;
    [SerializeField] private int _instructionsNumber = 0;

    private List<string> _gnatTypes;
    private List<string> _sparkTypes;

    // Dictionary<string, bool> _gnatTypes;

    private int _levelSet = 0;

    //public LevelSettings(int levelNumber, bool hasSparks, bool hasGnats, string sparkType, string gnatType, int numberOfGnats)
    //{
    //    _levelNumber = levelNumber;
    //    _hasSparks = hasSparks;
    //    _hasGnats = hasGnats;
    //    _sparkType = sparkType;
    //    _gnatType = gnatType;
    //    _numberOfGnats = numberOfGnats;
    //}

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    public int GetLevelSetNumber()
    {
        return _levelSet;
    }

    public bool GetIfHasInstructions()
    {
        return _hasInstructions;
    }

    public int GetInstructionsNumber()
    {
        return _instructionsNumber;
    }

    public List<string> GetSparkTypes()
    {
        _sparkTypes = new List<string>();

        if (_normalSparx)
            _sparkTypes.Add("normal");
        if (_textSparx)
            _sparkTypes.Add("text");

        return _sparkTypes;
    }

    public List<string> GetGnatTypes()
    {
        _gnatTypes = new List<string>();

        if (_normalGnat)
            _gnatTypes.Add("normal");
        if (_textGnat)
            _gnatTypes.Add("text");
        if (_fastGnat)
            _gnatTypes.Add("fast");
        if (_tankGnat)
            _gnatTypes.Add("tank");

        return _gnatTypes;
    }

    public int GetNumberOfGnats()
    {
        return _numberOfGnats;
    }

    public override string ToString()
    {
        return _levelNumber + ", " + _numberOfGnats;
    }

    public void SetLevelSetNumber(int num)
    {
        _levelSet = num;
    }
}
