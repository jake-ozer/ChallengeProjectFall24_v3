using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rank", menuName = "ScriptableObjects/RankScriptableObject", order = 1)]
public class Rank : ScriptableObject
{
    public Sprite icon;
    public float timeNeeded;
    
    public void setTime(float time)
    {
        this.timeNeeded = time;
    }
}
