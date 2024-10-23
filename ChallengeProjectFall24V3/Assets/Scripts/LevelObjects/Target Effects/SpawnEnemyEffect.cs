using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyEffect : MonoBehaviour, ITargetEffect
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float effectStartDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect(int timer)
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
