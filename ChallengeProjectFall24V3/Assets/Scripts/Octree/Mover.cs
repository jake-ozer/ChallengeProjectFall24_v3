using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Octree {
    public class Mover : MonoBehaviour
    {
        private float speed = 10f;
        private float accuracy = 1f;
        private float turnSpeed = 5f;

        private float timeOut = 2f;
        private float countDown;

        private int currentWaypoint;
        private OctreeNode currentNode;
        private Vector3 destination;

        public OctreeGenerator octreeGenerator;
        private Graph graph;

        public GameObject player;
        
        void Start()
        {
            graph = octreeGenerator.waypoints;
            currentNode = GetClosestNode(transform.position);
            GetRandomDestination();
        }

        OctreeNode GetClosestNode(Vector3 position) => octreeGenerator.tree.FindClosestNode(position);

        void GetRandomDestination()
        {
            OctreeNode destinationNode;
            do
            {
                destinationNode = graph.nodes.ElementAt(Random.Range(0, graph.nodes.Count)).Key;
            } while (!graph.AStar(currentNode, destinationNode));

            Debug.Log(graph.nodes.Count);
            currentWaypoint = 0;
        }

        private void OnDrawGizmos()
        {
            if (graph == null || graph.GetPathLength() == 0) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(graph.GetPathNode(0).bounds.center, 0.7f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(graph.GetPathNode(graph.GetPathLength() - 1).bounds.center, 0.7f);

            Gizmos.color = Color.green;
            for (int i = 0; i < graph.GetPathLength(); i++)
            {
                Gizmos.DrawWireSphere(graph.GetPathNode(i).bounds.center, 0.5f);
                if (i < graph.GetPathLength() - 1)
                {
                    Vector3 start = graph.GetPathNode(i).bounds.center;
                    Vector3 end = graph.GetPathNode(i + 1).bounds.center;
                    Gizmos.DrawLine(start, end);
                }
            }
        }

        void Update()
        {
            if (graph == null) return;

            if (graph.GetPathLength() == 0 || currentWaypoint >= graph.GetPathLength())
            {
                // GetRandomDestination();
                // Debug.Log(player.transform.position);
                // Debug.Log(GetClosestNode(player.transform.position).bounds.center);
                if (countDown <= 0f)
                {
                    var playerPos = player.transform.position;
                    if (!graph.AStar(currentNode, GetClosestNode(new Vector3(playerPos.x, playerPos.y + 5, playerPos.z))))
                    {
                        countDown = timeOut;
                    }
                }

                countDown -= Time.deltaTime;
                currentWaypoint = 0;
                return;
            }
            
            if (Vector3.Distance(graph.GetPathNode(currentWaypoint).bounds.center, transform.position) < accuracy)
            {
                currentWaypoint++;
                Debug.Log($"Waypoint {currentWaypoint} reached");
            }
            
            if (currentWaypoint < graph.GetPathLength())
            {
                currentNode = graph.GetPathNode(currentWaypoint);
                destination = currentNode.bounds.center;
            
                Vector3 direction = destination - transform.position;
                direction.Normalize();
            
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    turnSpeed * Time.deltaTime);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
    }
}
