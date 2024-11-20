using System;
using UnityEngine;

namespace Octree
{

    public class OctreeGenerator : MonoBehaviour
    {
        public GameObject[] objects;
        public float minNodeSize = 1f;
        public Octree tree;

        public readonly Graph waypoints = new();

        void Awake() => tree = new Octree(objects, minNodeSize, waypoints);

        public void OnDrawGizmos()
        {
            // if (!Application.isPlaying) return;
            // Gizmos.color = Color.green;
            // Gizmos.DrawWireCube(tree.bounds.center, tree.bounds.size);
            
            // tree.root.DrawNode();
            // tree.graph.DrawGraph();
        }
    }

}