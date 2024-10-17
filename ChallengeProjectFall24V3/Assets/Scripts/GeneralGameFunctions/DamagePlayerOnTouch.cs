using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageCooldown;
    //private bool active = true;
    private float coolDownStart;


    private void Awake()
    {
        coolDownStart = damageCooldown;
        damageCooldown = 0;
    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;

        if(damageCooldown < 0)
        {
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            damageCooldown = coolDownStart;
        }
    }

/*    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null && !active)
        {
            active = true;
        }
    }*/
}
