using UnityEngine;

namespace Player
{
    public class InverseKinematicsTargetCameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private float _needDistance;
        private Transform _cashedTransform;
        private Vector3 _needDirection;

        private void Awake()
        {
            _cashedTransform = transform;
        }

        private void Update()
        {
            _needDirection = Vector3.Reflect(_cameraTransform.forward, _bodyTransform.up);
            _cashedTransform.position = _needDistance * _needDirection + _bodyTransform.position;
        }
    }
}