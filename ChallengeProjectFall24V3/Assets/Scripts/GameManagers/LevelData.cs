using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    private Rank rank; 
    private float bestTime;
    
    public LevelData(float time, Rank rank)
    {
        this.bestTime = time;
        this.rank = rank;
    }

    public float GetBestTime()
    {

        return this.bestTime;
        
    }

    public Rank GetBestRank()
    {
        return this.rank;
    }

    
}
