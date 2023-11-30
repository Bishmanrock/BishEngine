using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

// Test collider class, for collisions between objects

namespace Engine
{
    public class Collider
    {
        private float[] vertices;
        private GameObject owner;

        public Collider(GameObject owner)
        {
            this.owner = owner;
        }

        public void SetVertices(float[] vertices)
        {
            this.vertices = vertices;
        }

        public bool isColliding(Collider other)
        {
            // Calculate AABB for this collider
            float minX = owner.transform.position.x - owner.transform.scale.x / 2;
            float maxX = owner.transform.position.x + owner.transform.scale.x / 2;
            float minY = owner.transform.position.y - owner.transform.scale.y / 2;
            float maxY = owner.transform.position.y + owner.transform.scale.y / 2;
            float minZ = owner.transform.position.z - owner.transform.scale.z / 2;
            float maxZ = owner.transform.position.z + owner.transform.scale.z / 2;

            // Calculate AABB for the other collider
            float otherMinX = other.owner.transform.position.x - other.owner.transform.scale.x / 2;
            float otherMaxX = other.owner.transform.position.x + other.owner.transform.scale.x / 2;
            float otherMinY = other.owner.transform.position.y - other.owner.transform.scale.y / 2;
            float otherMaxY = other.owner.transform.position.y + other.owner.transform.scale.y / 2;
            float otherMinZ = other.owner.transform.position.z - other.owner.transform.scale.z / 2;
            float otherMaxZ = other.owner.transform.position.z + other.owner.transform.scale.z / 2;

            // Check for overlap in all three dimensions
            bool xOverlap = minX <= otherMaxX && maxX >= otherMinX;
            bool yOverlap = minY <= otherMaxY && maxY >= otherMinY;
            bool zOverlap = minZ <= otherMaxZ && maxZ >= otherMinZ;

            // If there is overlap in all dimensions, the colliders are colliding
            return xOverlap && yOverlap && zOverlap;
        }
    }
}