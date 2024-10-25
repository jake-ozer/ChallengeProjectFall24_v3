using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBullet : MonoBehaviour
{

    private void OnTriggerEnter(Collision other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit!");
            Destroy(gameObject);
        }
    }


}
