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
    private bool first = true;


    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        ShootAtPlayer();
        transform.LookAt(Player.transform.position);
    }

    

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;
        //Doesn't shoot instantly.
        first = false;
        if(first == false)
        {
            bulletTime = timer;

            GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
            Destroy(bulletObj, 5f);
        }
        
    }



}
