using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    private Dictionary<string, LevelData> levelDict;
    private HashSet<string> playableLevels = new HashSet<string>();
    private SceneTransition sceneTransition;

    //translating save data
    public Rank bronze;
    public Rank silver;
    public Rank gold;
    public Rank plat;

    private void Awake()
    {
        //initialize dict
        levelDict = new Dictionary<string, LevelData>();
        
        //singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        sceneTransition = transform.Find("SceneTransition").GetComponent<SceneTransition>();

        //load level data from game files on game startup
        //not in web build
        //LoadLevelData();
    }

    /*
     * -pass in (level name, time, rank), creates a LevelData obj, and stores that in dict
     * -validates that time is actually faster than previous.
     * -it is safe to call this repeatedely from RankManager, no matter what time is
     */
    public void AddNewBestTime(string lvlName, float time, Rank rank)
    {
        //check if level is stored on dict
        if (levelDict.ContainsKey(lvlName))
        {
            //check if time is better
            LevelData prevData = levelDict[lvlName];
            float prevTime = prevData.GetBestTime();
            if(time < prevTime)
            {
                //replace level data with new one
                LevelData ld = new LevelData(time, rank);
                levelDict[lvlName] = ld;
            }
        }
        else //add it to the dict
        {
            LevelData ld = new LevelData(time, rank);
            levelDict.Add(lvlName, ld);
        }

        //save data to game files
        //dont save in web build
        //SaveLevelData();
    }

    //pass in level name, get best rank
    public Rank GetBestRank(string lvlName)
    {
        if (levelDict.ContainsKey(lvlName))
        {
            return levelDict[lvlName].GetBestRank();
        }
        return null;
    }

    public float GetBestTime(string lvlName)
    {
        if (levelDict.ContainsKey(lvlName))
        {
            return levelDict[lvlName].GetBestTime();
        }
        return 0f;
    }

    //used by level select components to check if a level is playable. Should add the name of the next level upon completion of the previous
    public void AddLevelToPlayableSet(string name)
    {
        playableLevels.Add(name);
    }

    //checks if a specified level is playable. used by level select components
    public bool isPlayable(string lvlName)
    {
        return playableLevels.Contains(lvlName);
    }

    //routes change scene to scene transition
    public void ChangeScene(string levelName)
    {
        sceneTransition.ChangeScene(levelName);
    }

    public void ChangeScene(int levelIndex)
    {
        sceneTransition.ChangeScene(levelIndex);
    }

    //bool value for if a level has data(has been played before) for it or not
    public bool HasPlayed(string lvlName)
    {

        return levelDict.ContainsKey(lvlName);

    }

    private void SaveLevelData()
    {
        //add all the times to a storage dict (we cant store icons)
        Dictionary<string, float> timeDict = new Dictionary<string, float>();
        Dictionary<string, string> rankNameDict = new Dictionary<string, string>();
        foreach (KeyValuePair<string, LevelData> kvp in levelDict)
        {
            timeDict.Add(kvp.Key, kvp.Value.GetBestTime());
            //Debug.Log("saving name: "+kvp.Value.GetBestRank().name);
            rankNameDict.Add(kvp.Key, kvp.Value.GetBestRank().name);
        }
        //save data to the file in save system
        LevelSaveData lsd = new LevelSaveData(timeDict, rankNameDict, this.playableLevels);
        SaveSystem.SaveLevelData(lsd);
    }

    private void LoadLevelData()
    {
        //pull data from save system
        LevelSaveData grabbedData = SaveSystem.LoadLevelData();
        this.playableLevels = grabbedData.savedPlayableLevels;
        //assign appropriate level data objects to all of the saved times we have
        Dictionary<string, float> grabbedTimeDict = grabbedData.savedLevelTimesDict;
        Dictionary<string, string> grabbedNameDict = grabbedData.savedLevelRankNamesDict;
        Dictionary<string, LevelData> tempDict = new Dictionary<string, LevelData>();
        foreach (KeyValuePair<string, float> kvp in grabbedTimeDict)
        {
            LevelData ld = new LevelData(kvp.Value, GetRankFromName(grabbedNameDict[kvp.Key]));
            tempDict.Add(kvp.Key, ld);
        }
        this.levelDict = tempDict;
    }

    //used for saving data
    private Rank GetRankFromName(string name)
    {
       switch(name)
       {
            case "Platinum":
                return this.plat;
            case "Gold":
                return this.gold;
            case "Silver":
                return this.silver;
            case "Bronze":
                return this.bronze;
            default:
                return this.bronze;
       }
    }
}
