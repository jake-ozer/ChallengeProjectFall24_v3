using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    public bool canChase;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //disable it at start so it doesnt jitter
        agent.enabled = false;
        target = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    private void Update()
    {
        //observe EnemyVision, if seen once, chase player forever after
        if (GetComponent<EnemyVision>().canSeePlayer)
        {
            canChase = true;
        }

        if (target != null && canChase) {
            agent.enabled = true;
            agent.SetDestination(target.position);
        }
    }
}
