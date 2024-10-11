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
        //CalculateMedalTime(bronzeTimeSeconds);
        startEnemies += FindObjectOfType<EnemySpawner>().GetEnemySpawnWall().transform.childCount; //Gets all spawn point enemies (enemies that haven't spawned yet)
        this.SetTotalEnemies(startEnemies);

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
    private void CalculateMedalTime(float bronzeTime)
    {
        //Example times, Bronze is 2:00 (120 seconds), then silver is (1:45), gold is (1:40), plat is (1:30)
        //Silver is 7/8th of bronze, then gold is 7/8 - 1/24, then plat is 3/4 of bronze.

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
