using UnityEngine;

namespace Common
{
    public interface IReadonlyLook
    {
        public Vector3 LookPointPosition { get; }
        public Vector3 LookDirection { get; }
    }
}