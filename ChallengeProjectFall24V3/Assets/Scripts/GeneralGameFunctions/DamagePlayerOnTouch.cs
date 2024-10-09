using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null && active)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            active = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null && !active)
        {
            active = true;
        }
    }
}
