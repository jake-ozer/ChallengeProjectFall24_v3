using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOOB : MonoBehaviour
{
    [SerializeField]
    private GameObject lastGround;
    [SerializeField]
    private float bufferCheckDistance;

    private bool oob;


    // Start is called before the first frame update
    void Start()
    {
        oob = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
    }

    private void GroundCheck() //Checks and saves the last ground object the player was standing on
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, bufferCheckDistance))
        {
            //Ground layer = 9
            if(hit.transform.gameObject.layer == 9)
            {
                lastGround = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.layer == 11 && !oob)
            {
                StartCoroutine(Lakitu());
                bool oob = true;
            }
        }
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.transform.gameObject.layer == 11 && !oob)
        {
            StartCoroutine(Lakitu());
            bool oob = true;
        }
    } */

    private IEnumerator Lakitu() //Placeholder method that returns player to last ground
    {
        Debug.Log("Lakitu");
        transform.position = new Vector3(lastGround.transform.position.x, lastGround.transform.position.y + bufferCheckDistance, lastGround.transform.position.z);
        yield return new WaitForSeconds(1);
        oob = false;
    }
  
}
