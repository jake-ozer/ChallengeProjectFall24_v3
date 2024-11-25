using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float radius;
    [Range(0, 360)] public float angle;

    public GameObject player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Awake()
    {
        var playerMove = FindObjectOfType<PlayerMovement>();
        if (playerMove != null)
            player = playerMove.gameObject;
    }

    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 targetDir = (target.position - transform.position).normalized;
            
            if(Vector3.Angle(transform.forward, targetDir) < angle / 2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);

                if(Physics.Raycast(transform.position, targetDir, targetDistance, obstructionMask))
                {
                    canSeePlayer = false;
                }
                else
                {
                    canSeePlayer = true;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
