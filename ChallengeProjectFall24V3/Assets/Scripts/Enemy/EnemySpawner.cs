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
    private bool trigger = false;
    private bool spawnObj = false;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!trigger) {
                
                int spawners = enemySpawnWall.transform.childCount;
                Debug.Log("Collision Triggered!");
                for (int i = 0; i < spawners; i++)
                {
                    Instantiate(enemyPrefab, transform.GetChild(i));
                }

                trigger = true; //Make it so it only triggers once.
                
            }
        }
    }

    public Collider GetEnemySpawnWall()
    {
        return this.enemySpawnWall;
    }


}
