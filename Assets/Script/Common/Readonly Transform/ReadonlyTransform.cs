using UnityEngine;

namespace Common.Readonly_Transform
{
    public class ReadonlyTransform : IReadonlyTransform
    {
        private readonly Transform _transform;

        public ReadonlyTransform(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 Position => _transform.position;
        public Quaternion Rotation => _transform.localRotation;
        public Vector3 LocalScale => _transform.localScale;
    }
}