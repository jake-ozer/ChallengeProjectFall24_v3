using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGizmo : MonoBehaviour
{

    [SerializeField] private Vector3 gizmoSize;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}