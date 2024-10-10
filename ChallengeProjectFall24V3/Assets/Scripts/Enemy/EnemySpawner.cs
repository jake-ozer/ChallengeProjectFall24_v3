using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Windows;



public class EnemySpawner : MonoBehaviour
{

    private MainInput input;
    [SerializeField] private Collider enemySpawnWall;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawnPt;
    private bool trigger = false;
    private bool spawnObj = false;

   

    private void OnTriggerEnter(Collider other)
    {
        if(!trigger)
        {
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
