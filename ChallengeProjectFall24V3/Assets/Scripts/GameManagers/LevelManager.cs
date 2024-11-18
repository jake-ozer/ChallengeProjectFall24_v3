using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private float playerBestTime = 0f;
    public static LevelManager instance { get; private set; }
    public LevelData[] lvlDataArr = new LevelData[SceneManager.sceneCountInBuildSettings-1];
    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) //skips tutorial level data
        {
            lvlDataArr[i-1] = new LevelData(0); 
        }
        DontDestroyOnLoad(instance);
    }

    public void setPlayerBestTime(float time)
    {
        instance.playerBestTime = time;
    }

    public void setPlayerBestRank(Rank rank)
    {
        //instance.playerBestRank = rank;
    }

    public void addTime(float time, int sceneIndex, string rank)
    {
    
        lvlDataArr[sceneIndex].addTime(rank, time);
    }



}
