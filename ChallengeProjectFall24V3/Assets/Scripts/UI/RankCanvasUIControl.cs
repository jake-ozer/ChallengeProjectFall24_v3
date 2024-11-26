using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankCanvasUIControl : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject endMenu;
    private RankManager rankManager;
    public GameObject newBestAttemptMenu;

    //Start Menu GameObjects
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI bronzeBenchmarkTime;
    public TextMeshProUGUI silverBenchmarkTime;
    public TextMeshProUGUI goldBenchmarkTime;
    public TextMeshProUGUI platBenchmarkTime;
    public TextMeshProUGUI bestTime;
    public Image rankDisplay;
    
    //End Menu GameObjects
    //Current Attempt Info
    public TextMeshProUGUI timeAchieved;
    public Image rankAchieved;
    //Best Time Achieved
    public Image bestRankAchieved;
    public TextMeshProUGUI bestTimeAchieved;
   
    //NEW HIGH SCORE 
    public TextMeshProUGUI prevTime;
    public Image prevRank;





    private void Start() //Starts on every scene.
    {
        rankManager = FindObjectOfType<RankManager>();
        levelName.text = rankManager.levelName;
        bool hasPlayed = LevelManager.instance.HasPlayed(rankManager.levelName);
        bronzeBenchmarkTime.text = convertFloatToMinutes(rankManager.bronzeTimeSeconds);
        silverBenchmarkTime.text = convertFloatToMinutes(rankManager.silverTimeSeconds);
        goldBenchmarkTime.text = convertFloatToMinutes(rankManager.goldTimeSeconds);
        platBenchmarkTime.text = convertFloatToMinutes(rankManager.platTimeSeconds);
        bestTime.text = "0s";


        if(hasPlayed)
        {
            if (!rankDisplay.gameObject.activeSelf)
            {
                rankDisplay.gameObject.SetActive(true);
            }
            rankDisplay.sprite = LevelManager.instance.GetBestRank(rankManager.levelName).icon;
            bestTime.text = convertFloatToMinutes(LevelManager.instance.GetBestTime(rankManager.levelName));
            
        }

        StartCoroutine("EnableStartMenu");




    }
    public IEnumerator EnableStartMenu()
    {
        
        startMenu.SetActive(true);
        yield return new WaitForSeconds(2f);
        startMenu.SetActive(false);
        
    }


    public void EnableEndMenu(bool newHighScore)
    {

        timeAchieved.text = convertFloatToMinutes(rankManager.timer);
        rankAchieved.sprite = rankManager.getRank(rankManager.timer).icon;
        rankAchieved.gameObject.SetActive(true);
        bestTimeAchieved.text = convertFloatToMinutes(LevelManager.instance.GetBestTime(rankManager.levelName));
        if(LevelManager.instance.GetBestRank(rankManager.levelName) != null)
        bestRankAchieved.sprite = LevelManager.instance.GetBestRank(rankManager.levelName).icon;
        
        

        //Extra guidelines to make sure that current attempt has atleast 1 previous attempt before.
        //rM.prevTime and rM.prevRank are initalized to 999f and null, respectively in RankManager script. 
        
        if (newHighScore && rankManager.prevTime != 0f && rankManager.prevRank != null) 
        {
            prevTime.text = convertFloatToMinutes(rankManager.prevTime);
            prevRank.sprite = rankManager.prevRank.icon;
            newBestAttemptMenu.SetActive(true);
        } else if(newHighScore && rankManager.prevTime == 0f && rankManager.prevRank == null)
        {
            //If these inputs haven't changed, then that means this is user's first time so it should display 0 seconds/no prev rank.
            prevTime.text = "0s";
            newBestAttemptMenu.SetActive(true);
        }


        endMenu.SetActive(true);

    }

    public string convertFloatToMinutes(float val)
    {   
            TimeSpan time = TimeSpan.FromSeconds(val);
            return time.ToString("mm':'ss"); 
        
    }
}
