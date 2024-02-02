using System.Collections;
using Common.Interfaces;
using Common.Readonly_Transform;
using DG.Tweening;
using Player.Look.Settings;
using UnityEngine;

namespace Player.Look
{
    public class PlayerLook : IPlayerLook
    {
        private readonly IReadonlyTransform _cameraRootTransform;
        private readonly Transform _cameraTransform;
        private readonly IPlayerLookSettings _lookSettings;
        private readonly Transform _bodyRotateObject;
        private readonly GameObject _linkGameObject;
        private readonly ICoroutineStarter _coroutineStarter;
        private RaycastHit _cameraForwardRaycastHit;

        private float _xRotation;
        private bool _isCameraLockedOnPoint;
        private Vector3 _lookLockPoint;
        private Sequence _startLockedPointRotationSequence;

        public PlayerLook(Camera camera, IReadonlyTransform cameraRootTransform, Transform bodyRotateObject,
            IPlayerLookSettings lookSettings, ICoroutineStarter coroutineStarter)
        {
            _cameraRootTransform = cameraRootTransform;
            _bodyRotateObject = bodyRotateObject;
            _lookSettings = lookSettings;
            _cameraTransform = camera.transform;
            _coroutineStarter = coroutineStarter;
            _linkGameObject = _cameraTransform.gameObject;
        }

        public Vector3 LookPointPosition
        {
            get
            {
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _cameraForwardRaycastHit,
                        _lookSettings.MaxAimRaycastDistance, _lookSettings.AimLayerMask,
                        QueryTriggerInteraction.Ignore))
                {
                    return _cameraForwardRaycastHit.point;
                }

                return _cameraTransform.position + _cameraTransform.forward * _lookSettings.MaxAimRaycastDistance;
            }
        }

        public Vector3 LookDirection => _cameraTransform.forward;

        public bool TryCalculateLookPointWithSphereCast(out Vector3 lookPoint, float maxDistance, float sphereRadius,
            LayerMask layerMask)
        {
            if (Physics.SphereCast(_cameraTransform.position, sphereRadius, _cameraTransform.forward,
                    out _cameraForwardRaycastHit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                lookPoint = _cameraForwardRaycastHit.point;
                return true;
            }

            lookPoint = Vector3.zero;
            return false;
        }

        public void LookInputtedWith(Vector2 mouseLookDelta)
        {
            if (_isCameraLockedOnPoint)
            {
                return;
            }
            _cameraTransform.position = _cameraRootTransform.Position;
            _xRotation -= mouseLookDelta.y;
            _xRotation = Mathf.Clamp(_xRotation, _lookSettings.UpperLimit, _lookSettings.BottomLimit);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _bodyRotateObject.Rotate(Vector3.up, mouseLookDelta.x);
        }

        // TODO: improve rotation calcilation
        public void StartLookingAtPoint(Vector3 lookPoint)
        {
            _isCameraLockedOnPoint = true;
            _lookLockPoint = lookPoint;
            _startLockedPointRotationSequence?.Kill();
            _startLockedPointRotationSequence
                .Append(_cameraTransform.DORotateQuaternion(CalculateLockedLookHeadRotation(),
                    _lookSettings.LookAtStartAnimationDuration))
                .Join(_bodyRotateObject.DORotateQuaternion(CalculateLockedLookBodyRotation(),
                    _lookSettings.LookAtStartAnimationDuration))
                .SetLink(_linkGameObject)
                .SetEase(_lookSettings.LookAtStartAnimationEase)
                .OnComplete(() => { _coroutineStarter.StartCoroutine(LockAtHookPoint()); });
        }

        public void StopLookingAtPoint()
        {
            _isCameraLockedOnPoint = false;
            _startLockedPointRotationSequence?.Kill();
        }

        private IEnumerator LockAtHookPoint()
        {
            while (_isCameraLockedOnPoint)
            {
                _bodyRotateObject.rotation = CalculateLockedLookBodyRotation();
                _cameraTransform.rotation = CalculateLockedLookHeadRotation();
                yield return null;
            }
        }

        private Quaternion CalculateLockedLookBodyRotation()
        {
            var rotateObjectRotation = Quaternion.LookRotation(_lookLockPoint - _bodyRotateObject.position);
            var originalRotation = _bodyRotateObject.rotation;
            rotateObjectRotation.x = originalRotation.x;
            rotateObjectRotation.z = originalRotation.z;
            return rotateObjectRotation;
        }
        
        private Quaternion CalculateLockedLookHeadRotation()
        {
            var cameraRotation = Quaternion.LookRotation(_lookLockPoint - _cameraTransform.position);
            var originalRotation = _cameraTransform.rotation;
            cameraRotation.x = originalRotation.x;
            cameraRotation.y = originalRotation.y;
            return cameraRotation;
        }
    }
}