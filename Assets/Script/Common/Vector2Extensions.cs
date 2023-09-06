using UnityEngine;

namespace Common
{
    public static class Vector3Extensions
    {
        public static int CompareTo(this Vector3 thisVector, Vector3 otherVector)
        {
            int xComparison = thisVector.x.CompareTo(otherVector.x);
            if (xComparison != 0)
            {
                return xComparison;
            }

            int yComparison = thisVector.y.CompareTo(otherVector.y);
            return yComparison != 0 ? yComparison : thisVector.z.CompareTo(otherVector.z);
        }
    }
}