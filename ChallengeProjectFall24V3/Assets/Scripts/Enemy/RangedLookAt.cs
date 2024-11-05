using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedLookAt : MonoBehaviour
{
    public GameObject PlayerCam;
   

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerCam.transform.position);
    }
}
