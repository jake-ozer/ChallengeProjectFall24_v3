using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemy : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private float timer = 5;
    [SerializeField] private float timeTillStartShooting;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;
    public bool canShoot = false;


    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        bulletTime = timeTillStartShooting;
    }


    // Update is called once per frame
    void Update()
    {
        //observe EnemyVision, if seen once, shoot player forever after
        if (GetComponent<EnemyVision>().canSeePlayer)
        {
            canShoot = true;
        }

        if (canShoot)
        {
            ShootAtPlayer();
            transform.LookAt(Player.transform.position);
        }
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
