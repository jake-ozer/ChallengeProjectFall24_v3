using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private LayerMask enemyLayer;
    private Animator gunAnim;
    public Transform camTransform;
    public ShotgunPellet pelletPrefab;

    [SerializeField] private Vector3 _muzzleOffset = new Vector3(0, 1.0f, 0);
    [SerializeField] private uint _numPellets = 5;
    [SerializeField] private float _pelletSpeed = 50f;
    [SerializeField] private float _spreadAngle = 15f;

    private uint _numPelletsSqrt;

    private void Start()
    {
        gunAnim = GetComponent<Animator>();
        camTransform = Camera.main.transform;
        _numPelletsSqrt = (uint)Mathf.Sqrt(_numPellets);
    }

    public void Shoot()
    {
        gunAnim.SetTrigger("Shoot");

        Vector3 offset = new Vector3(0, 0, 0);

        Vector3 muzzlePosition = transform.TransformPoint(_muzzleOffset);
        for (uint i = 0; i < _numPelletsSqrt; i++)
        {
            for (uint j = 0; j < _numPelletsSqrt; j++)
            {
                offset.x = i * 0.1f;
                offset.y = j * 0.1f;
                ShotgunPellet pellet = Instantiate(pelletPrefab, muzzlePosition + offset, Quaternion.identity);
            
                Vector3 spreadDirection = Random.insideUnitSphere * Mathf.Tan(_spreadAngle * Mathf.Deg2Rad);
                Vector3 shootDirection = camTransform.forward + spreadDirection;
                shootDirection.Normalize();

                pellet.GetComponent<Rigidbody>().velocity = shootDirection * _pelletSpeed;
            }
        }
    }

    private void Update()
    {
        //shooting visualizer
        // Debug.DrawRay(camTransform.position, camTransform.forward * 10000f, Color.red);
    }
}