using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1,1,1));
    }
}
