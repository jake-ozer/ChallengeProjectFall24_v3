using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSceneTrigger : MonoBehaviour
{
    private RankManager rankMng;

    private void Start()
    {
        rankMng = FindObjectOfType<RankManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rankMng.setEndLevel(true);
        }
    }

    
}
