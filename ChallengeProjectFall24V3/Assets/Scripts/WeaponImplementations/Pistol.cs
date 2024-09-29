using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage;
    private Animator gunAnim;
    public Transform camTransform;

    private void Start()
    {
        gunAnim = GetComponent<Animator>();
        camTransform = Camera.main.transform;
    }

    public void Shoot()
    {
        gunAnim.SetTrigger("Shoot");

        RaycastHit hit;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, enemyLayer))
        {
            Debug.Log("You hit "+hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        //shooting visualizer
        Debug.DrawRay(camTransform.position, camTransform.forward * 10000f, Color.red);
    }
}
