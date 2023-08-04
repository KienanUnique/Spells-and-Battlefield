using UnityEngine;

namespace Common.Readonly_Transform
{
    public interface IReadonlyTransform
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 LocalScale { get; }
        public Vector3 Up { get; }
        public Vector3 Forward { get; }
    }
}