using System;
using UnityEngine;

namespace Common
{
    public static class Vector3Extensions
    {
        public static int CompareTo(this Vector3 vector, object obj)
        {
            if (obj == null) return 1;

            if (!(obj is Vector3 otherVector))
            {
                throw new ArgumentException("Object is not a Vector3", nameof(obj));
            }

            var xComparison = vector.x.CompareTo(otherVector.x);
            if (xComparison != 0) return xComparison;

            var yComparison = vector.y.CompareTo(otherVector.y);
            return yComparison != 0 ? yComparison : vector.z.CompareTo(otherVector.z);
        }
    }
}