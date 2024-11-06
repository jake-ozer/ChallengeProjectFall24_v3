using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int damageDealt;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip deathSound;
    private SpeedState spdState;
    //boolean to control mutual exclusion so that die doesnt get called multiple times
    private bool alreadyDead = false;
   


    private void Awake()
    {
        healthBar.SetMaxHealth(health);
        spdState = FindObjectOfType<SpeedState>();
        
    }

    /// <summary>
    /// Enemy loses health equal to pos number
    /// </summary>
    /// <param name="dmg">positive number</param>
    public void Hit(int dmg)
    {
        health -= dmg;
        if(healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!alreadyDead)
        {
            alreadyDead = true;

            //Debug.Log("Die method called");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(explosionSound);
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            spdState.UpdateSpeedState(true);
            //disable all children (and its own box collider), then destroy itself
            GetComponent<BoxCollider>().enabled = false;
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name != "EnemyCanvas")
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                } 
                else
                {
                    DisableEnemyCanvasSafely(transform.GetChild(i).gameObject);
                }
            }

            //if its a ranged enemy, disable shooting
            RangedEnemy rangedEnemy = GetComponent<RangedEnemy>();
            if(rangedEnemy != null){
                rangedEnemy.enabled = false;
            }


            Invoke("DestroyAfterTime", 2f);
        }
    }

    private void DestroyAfterTime()
    {
        Destroy(gameObject);
    }

    public int getDamageDealt()
    {
        return damageDealt;
    }

    //there is an issue with cancelling the healthbar easing coroutine, so we have to leave the healthbar controller active, and disable everything else :P
    private void DisableEnemyCanvasSafely(GameObject canvas)
    {
        int childCount = canvas.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (canvas.transform.GetChild(i).gameObject.name != "HealthBarController")
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
