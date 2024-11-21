using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [SerializeField] public float bronzeTimeSeconds = 0;
    [SerializeField] public float silverTimeSeconds;
    [SerializeField] public float goldTimeSeconds;
    [SerializeField] public float platTimeSeconds;
    public Rank bronze;
    public Rank silver;
    public Rank gold;
    public Rank plat;
    private int startEnemies = 0;
    private bool platMedalCheck = false;
    private int totalEnemies;
    public float timer = 0;
    private float killCount = 0;
    private bool endLevel = false;
    public string levelName;
    private RankCanvasUIControl canvasControl;
    public float prevTime = 999f;
    public Rank prevRank = null;


    void Start()
    {
        Enemy[] preplacedEnemyList = FindObjectsOfType<Enemy>();
        if(FindObjectOfType<EnemySpawner>() != null)
        {
            startEnemies += FindObjectOfType<EnemySpawner>().GetEnemySpawnWall().transform.childCount;//Gets all spawn point enemies (enemies that haven't spawned yet)
        } 
        this.SetTotalEnemies(preplacedEnemyList.Length + startEnemies); //adds preplaced and enemies yet to be spawned
        this.bronze.setTime(bronzeTimeSeconds);
        this.silver.setTime(silverTimeSeconds);
        this.gold.setTime(goldTimeSeconds);
        this.plat.setTime(platTimeSeconds);
        canvasControl = FindObjectOfType<RankCanvasUIControl>();
        

     }


    // Update is called once per frame
    void Update() //SHOULD I USE COROUTINE FOR CANVAS STUFFFFFFFF
    {
        //Debug.Log(totalEnemies);
        if (endLevel == false) { 
        timer += Time.deltaTime;
        }
       
        if(killCount == totalEnemies && platMedalCheck == false)
        {
            platMedalCheck = true;  
        }

        if(endLevel == true && SceneManager.GetActiveScene().buildIndex != 0) //Not tutorial level
        {
            //---------JAKE COMMENTED THE LINE BELOW OUT BECAUSE HE CHANGED LEVEL MANAGER. CHANGE METHOD CALL TO USE THE NEW AddNewBestTime METHOD------------------
            
            //Temka changed method call
            if(LevelManager.instance.hasPlayed(levelName) == true) //If player has played the level before
            {
                prevTime = (float)Math.Round(LevelManager.instance.GetBestTime(levelName), 1); 
                prevRank = LevelManager.instance.GetBestRank(levelName);
                LevelManager.instance.AddNewBestTime(levelName, (float)Math.Round(timer, 1), getRank(timer));
                if (prevTime > timer) //If current time is faster than previous
                {
                    canvasControl.EnableEndMenu(true);
                    
                } else //If current time is slower than previous
                {
                    canvasControl.EnableEndMenu(false);
                    
                }
            } else //If player hasnt played level before
            {
                canvasControl.EnableEndMenu(true);
                LevelManager.instance.AddNewBestTime(levelName, (float)Math.Round(timer, 1), getRank(timer));
                
            }
        }
    }

    public Rank getRank(float time)
    {

        if (time < platTimeSeconds && platMedalCheck == true) //30, 45, 55
        {
            return this.plat;

        }
        else if (time < platTimeSeconds && platMedalCheck != true)
        {
            
            return this.gold;
            //return "gold"; //28 not enough kills
        }
        else if (time < goldTimeSeconds)
        {
            return this.gold;
            //return "gold"; //33 
        }
        else if (time < silverTimeSeconds)
        {
            return this.silver;
            //return "silver"; //50
        }
        else 
        {
            return this.bronze;
            //return "bronze";
        }

        

    }

    

    public void setEndLevel(bool update) {
    
            this.endLevel = update;
    }

    private void SetTotalEnemies(int num)
    {
        this.totalEnemies = num;
    }
    public void enemyCount(int i)
    {
        startEnemies++;
    }

    public void addKill(int i )
    {
        killCount++;
    }
}
