using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour, IWeapon
{
[SerializeField] private LayerMask enemyLayer;
[SerializeField] private int damage;
//[SerializeField] private Animator leftPistolAnim;
//[SerializeField] private Animator rightPistolAnim;
public Transform camTransform;
[SerializeField] private float stabRate;
//[SerializeField] AudioClip shootSFX;
//private AudioSource gunSource;
private float stabTimer = 0f;
private bool toggleKnifeAnim = true;

    public Sprite crosshair { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    private void Start()
    {
        camTransform = Camera.main.transform;
        //gunSource = transform.parent.GetComponent<AudioSource>()
    }

    public void Shoot()
    {
        if (stabTimer <= 0)
        {
            //gunSource.PlayOneShot(shootSFX);
            //Animator curAnim = togglePistolAnim ? leftPistolAnim : rightPistolAnim;
            //string shootType = togglePistolAnim ? "LeftShoot" : "RightShoot";
            //curAnim.Play(shootType, -1, 0f);

            //togglePistolAnim = !togglePistolAnim;

            RaycastHit hit;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 10f, enemyLayer))
            {
                Debug.Log("You hit " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ITakeHit>().Hit(damage);
            }

            stabTimer = stabRate;
        }
    }

    // Update is called once per frame
    private void Update()
    {
         //shoot timer
        stabTimer -= Time.deltaTime;
        
        //shooting visualizer
        Debug.DrawRay(camTransform.position, camTransform.forward * 10f, Color.red);
        
    }

    public void ResetAnimState()
    {
        //leftPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //rightPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //leftPistolAnim.SetTrigger("Reset");
        //4rightPistolAnim.SetTrigger("Reset");
    }

    public void ReEnableGFX()
    {
        //leftPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //rightPistolAnim.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}



