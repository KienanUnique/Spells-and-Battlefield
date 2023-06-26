using UnityEngine;

namespace Common
{
    public static class Vector3Extensions
    {
        public static int CompareTo(this Vector3 thisVector, Vector3 otherVector)
        {
            var xComparison = thisVector.x.CompareTo(otherVector.x);
            if (xComparison != 0) return xComparison;

            var yComparison = thisVector.y.CompareTo(otherVector.y);
            return yComparison != 0 ? yComparison : thisVector.z.CompareTo(otherVector.z);
        }
    }
}