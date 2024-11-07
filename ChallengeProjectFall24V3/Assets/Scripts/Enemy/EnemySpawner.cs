using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Windows;



public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Collider enemySpawnWall;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeTillSpawn;
    [SerializeField] private GameObject spawnEffect;
    private bool trigger = false;
    private bool spawnObj = false;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!trigger) {

                StartCoroutine("SpawnEnemies");

                trigger = true; //Make it so it only triggers once.
                
            }
        }
    }

    public Collider GetEnemySpawnWall()
    {
        return this.enemySpawnWall;
    }

    private IEnumerator SpawnEnemies()
    {
        //spawn a particle effect indicating where they will spawn
        int spawners = enemySpawnWall.transform.childCount;
        GameObject[] spawnEffects = new GameObject[spawners];
        
        for (int i = 0; i < spawners; i++)
        {
            spawnEffects[i] = Instantiate(spawnEffect, transform.GetChild(i));
        }

        //wait some time
        yield return new WaitForSeconds(timeTillSpawn);

        //delete particle effect
        foreach (GameObject effect in spawnEffects) { 
            Destroy(effect);
        }
        //spawn the actual enemy
        for (int i = 0; i < spawners; i++)
        {
            Instantiate(enemyPrefab, transform.GetChild(i));
        }
    }
}
