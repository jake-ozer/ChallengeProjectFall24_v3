using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    private Dictionary<string, LevelData> levelDict;
    private HashSet<string> playableLevels = new HashSet<string>();
    private SceneTransition sceneTransition;

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
}
