using Unity.VisualScripting;
using UnityEngine;

namespace Octree
{
    public class OctreeObject
    {
        public Bounds bounds;

        public OctreeObject(GameObject obj)
        {
            bounds = obj.GetComponent<Collider>().bounds;
        }

        public bool Intersects(Bounds otherBounds) => bounds.Intersects(otherBounds);
    }
}