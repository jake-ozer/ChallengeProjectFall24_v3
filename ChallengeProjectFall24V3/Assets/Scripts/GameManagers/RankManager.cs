using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

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


    void Start()
    {
        Enemy[] preplacedEnemyList = FindObjectsOfType<Enemy>();
        if(FindObjectOfType<EnemySpawner>() != null)
        {
            startEnemies += FindObjectOfType<EnemySpawner>().GetEnemySpawnWall().transform.childCount;//Gets all spawn point enemies (enemies that haven't spawned yet)
        } 
        this.SetTotalEnemies(preplacedEnemyList.Length + startEnemies); //adds preplaced and enemies yet to be spawned.

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(totalEnemies);
        timer += Time.deltaTime;
       
        if(killCount == totalEnemies)
        {
            Debug.Log("plat medal kill count reached");
            platMedalCheck = true;
        }



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
