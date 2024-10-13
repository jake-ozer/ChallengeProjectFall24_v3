using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int damageDealt;


    private void Awake()
    {
        healthBar.SetMaxHealth(health);
        spdState = FindObjectOfType<SpeedState>();
        FindObjectOfType<RankManager>().enemyCount(1); 
        
    }

    /// <summary>
    /// Enemy loses health equal to pos number
    /// </summary>
    /// <param name="dmg">positive number</param>
    public void TakeDamage(int dmg)
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
        Destroy(gameObject);
        spdState.UpdateSpeedState(true);

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
