using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Player Hit!");
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }
    }


}
