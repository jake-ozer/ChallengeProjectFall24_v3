using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CustomEditor(typeof(EnemyVision))]
public class EnemyVisionEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyVision ev = (EnemyVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ev.transform.position, Vector3.up, Vector3.forward, 360, ev.radius);

        Vector3 viewAngle1 = DirectionFromAngle(ev.transform.eulerAngles.y, -ev.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(ev.transform.eulerAngles.y, ev.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(ev.transform.position, ev.transform.position + viewAngle1 * ev.radius);
        Handles.DrawLine(ev.transform.position, ev.transform.position + viewAngle2 * ev.radius);

        if(ev.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(ev.transform.position, ev.player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
