using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    [SerializeField] private float bronzeTimeSeconds;
    [SerializeField] private float silverTimeSeconds;
    [SerializeField] private float goldTimeSeconds;
    [SerializeField] private float platTimeSeconds;
    private int startEnemies = 0;
    private bool platMedalCheck = false;
    private int totalEnemies;
    private float timer = 0;
    private float killCount = 0;
    private bool endLevel = false;


    void Start()
    {
        Enemy[] preplacedEnemyList = FindObjectsOfType<Enemy>();
        if(FindObjectOfType<EnemySpawner>() != null)
        {
            startEnemies += FindObjectOfType<EnemySpawner>().GetEnemySpawnWall().transform.childCount;//Gets all spawn point enemies (enemies that haven't spawned yet)
        } 
        this.SetTotalEnemies(preplacedEnemyList.Length + startEnemies); //adds preplaced and enemies yet to be spawned
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(totalEnemies);
        timer += Time.deltaTime;
       
        if(killCount == totalEnemies && platMedalCheck == false)
        {
            Debug.Log("plat medal kill count reached");
            platMedalCheck = true;
           
        }

        if(endLevel == true && SceneManager.GetActiveScene().buildIndex != 0) //Not tutorial level
        {

            LevelManager.instance.addTime(timer,SceneManager.GetActiveScene().buildIndex-1, getRank(timer));
            //displayEndMenu();
            //Sprite[] rankArr = Resources.LoadAll<Sprite>("Assets/Art/UI/RankIcons/rankicons.png");
            //Rank bronze = new Rank();
            //bronze.icon = rankArr[0];
            

        }
        


    }

    private void displayEndMenu()
    {



    }

    private string getRank(float time)
    {

        if (time < platTimeSeconds && platMedalCheck == true) //30, 45, 55
        {
            return "plat"; //28
        }
        else if (time < platTimeSeconds && platMedalCheck != true)
        {
            return "gold"; //28 not enough kills
        }
        else if (time < goldTimeSeconds)
        {
            return "gold"; //33 
        }
        else if (time < silverTimeSeconds)
        {
            return "silver"; //50
        }
        else 
        {
            return "bronze";
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
