using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int health;

    private void Awake()
    {
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            health -= collision.gameObject.GetComponent<Enemy>().getDamageDealt();
            healthBar.SetHealth(health);
        }
    }*/
}
