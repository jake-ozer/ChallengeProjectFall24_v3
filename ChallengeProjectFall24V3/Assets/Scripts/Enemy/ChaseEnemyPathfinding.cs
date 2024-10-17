using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    private void Update()
    {
        if (target != null) { 
            agent.SetDestination(target.position);
        }
    }
}
