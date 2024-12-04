using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health;
    public AudioClip takeDamageSFX;
    public float healthRegenTime;
    private float regenTimeStart;
    private float maxHealth;
    private float t;
    public float regenSpeed;
    private bool regenStarted = false;

    private void Awake()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
        regenTimeStart = healthRegenTime;
    }

    public void TakeDamage(int damage)
    {
        GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);

        health -= damage;
        healthBar.SetHealth(health);
        healthRegenTime = regenTimeStart;

        //player death
        if(health <= 0)
        {
            FindObjectOfType<LevelManager>().ChangeScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void Update()
    {
        if (health < maxHealth)
        {
            healthRegenTime -= Time.deltaTime;

            if (healthRegenTime <= 0)
            {
                RegenHealth();
            }
        }

        if (health == maxHealth)
        {
            healthRegenTime = regenTimeStart;
        }
    }

    private void RegenHealth()
    {
        health += Time.deltaTime * regenSpeed;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.SetHealth(health);
    }
}
