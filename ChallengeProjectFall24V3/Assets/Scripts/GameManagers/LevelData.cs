using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelData : MonoBehaviour
{

    private Rank rank; 
    private float bestTime;
    

    public LevelData(float time)
    {
        this.bestTime = time;
    }

    public void addRank(string rank)
    {
        if(rank.Equals("bronze"))
        {
            //this.rank.icon = Resources.Load<Sprite>("Assets/Art/UI/RankIcons/rankicons.png");
        } else if(rank.Equals("silver"))
        {

        } else if (rank.Equals("gold"))
        {
            
        } else
        {

        }


    }

    public bool addTime(string rank, float time)
    {
        //Returns true if new time is faster, return false if new time is slower.
        if(time < bestTime)
        {
            this.bestTime = time;
            this.addRank(rank);
            return true;
        }

        return false;


        //if(prevTimes.Count == 0) //If first time
        //{
        //    prevTimes.Add(time);
        //    this.bestTime = time;
        //} else if(prevTimes.Count == 1) //If only 1 time has been added.
        //{
        //    if (prevTimes[0] < time)
        //    {
        //        prevTimes.Add(time);
        //    } else
        //    {
        //        prevTimes.Insert(0, time);
        //    }
        //} else //If more than 1 time has been recorded for this.
        //{
        //    bool added = false;
        //    for (int i = 0; i < prevTimes.Count; i++)
        //    {

        //        if (prevTimes[i] < time) //if current time is faster than prevTime[i]
        //        {
        //            added = true;
        //            prevTimes.Insert(i, time); //add time
        //            if (i == 0)
        //            { //If new fastest time
        //                this.bestTime = time;
        //            }
        //            break;
        //        }

              
        //    }
        //    if(added == false)
        //    {
        //        prevTimes.Add(time);
        //    }
        //}
    }

    public float getBestTime()
    {

        return this.bestTime;
        
    }

    
}
