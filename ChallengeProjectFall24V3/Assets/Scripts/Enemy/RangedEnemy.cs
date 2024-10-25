using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemy : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private float timer = 5;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;
    
    

    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.LookAt(Player, Vector3.up);
        ShootAtPlayer();
    }

    

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
        Destroy(bulletObj, 5f);
    }



}
