using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int damageDealt;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip deathSound;
    private SpeedState spdState;


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
        healthBar.SetHealth(health);
    
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(explosionSound);
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        spdState.UpdateSpeedState(true);
        //disable all children, then destroy itself
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Invoke("DestroyAfterTime", 2f);

    }

    private void DestroyAfterTime()
    {
        Destroy(gameObject);
    }

    public int getDamageDealt()
    {
        return damageDealt;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
