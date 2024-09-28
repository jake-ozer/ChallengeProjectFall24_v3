using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    

/// <summary>
/// Enemy loses health equal to pos number
/// </summary>
/// <param name="dmg">positive number</param>
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    
    if (health <= 0)
    {
        Die();
    }
    }

    private void Die()
    {
        Destroy(gameObject);

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
