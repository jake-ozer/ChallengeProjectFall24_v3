using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage;
    [SerializeField] private Animator leftPistolAnim;
    [SerializeField] private Animator rightPistolAnim;
    public Transform camTransform;
    [SerializeField] private float fireRate;
    [SerializeField] AudioClip shootSFX;
    private AudioSource gunSource;
    private float shootTimer = 0f;
    private bool togglePistolAnim = true;
    

    private void Start()
    {
        camTransform = Camera.main.transform;
        gunSource = transform.parent.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            gunSource.PlayOneShot(shootSFX);
            Animator curAnim = togglePistolAnim ? leftPistolAnim : rightPistolAnim;
            string shootType = togglePistolAnim ? "LeftShoot" : "RightShoot";
            curAnim.Play(shootType, -1, 0f);

            togglePistolAnim = !togglePistolAnim;

            RaycastHit hit;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, enemyLayer))
            {
                Debug.Log("You hit " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ITakeHit>().Hit(damage);
            }

            shootTimer = fireRate;
        }
    }

    private void Update()
    {
        //shoot timer
        shootTimer -= Time.deltaTime;

        //shooting visualizer
        Debug.DrawRay(camTransform.position, camTransform.forward * 10000f, Color.red);
    }

    public void ResetAnimState()
    {
        leftPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = false;
        rightPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = false;
        leftPistolAnim.SetTrigger("Reset");
        rightPistolAnim.SetTrigger("Reset");
    }

    public void ReEnableGFX()
    {
        leftPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = true;
        rightPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
