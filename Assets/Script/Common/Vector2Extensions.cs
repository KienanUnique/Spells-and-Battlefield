using UnityEngine;

namespace Common
{
    public static class Vector2Extensions
    {
        public static int CompareTo(this Vector2 thisVector, Vector2 otherVector)
        {
            var xComparison = thisVector.x.CompareTo(otherVector.x);
            return xComparison != 0 ? xComparison : thisVector.y.CompareTo(otherVector.y);
        }
    }
}