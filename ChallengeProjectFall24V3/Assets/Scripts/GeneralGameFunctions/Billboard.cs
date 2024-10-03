using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (cam != null)
        {
            this.transform.LookAt(transform.position + cam.forward);
        }
    }
}
