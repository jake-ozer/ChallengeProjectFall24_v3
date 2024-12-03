using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSaveData
{
    public Dictionary<string, float> savedLevelTimesDict;
    public Dictionary<string, string> savedLevelRankNamesDict;
    public HashSet<string> savedPlayableLevels;

    public LevelSaveData(LevelSaveData lsd)
    {
        savedLevelTimesDict = lsd.savedLevelTimesDict;
        savedLevelRankNamesDict = lsd.savedLevelRankNamesDict;
        savedPlayableLevels = lsd.savedPlayableLevels;
    }

    public LevelSaveData(Dictionary<string, float> dict, Dictionary<string, string> nameDict, HashSet<string> set)
    {
        savedLevelTimesDict = dict;
        savedLevelRankNamesDict = nameDict;
        savedPlayableLevels = set;
    }
}
