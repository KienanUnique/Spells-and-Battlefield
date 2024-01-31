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
        private readonly Transform _rotateObject;
        private readonly GameObject _linkGameObject;
        private readonly ICoroutineStarter _coroutineStarter;
        private RaycastHit _cameraForwardRaycastHit;

        private float _xRotation;
        private bool _isCameraLockedOnPoint;
        private Vector3 _lockPoint;

        public PlayerLook(Camera camera, IReadonlyTransform cameraRootTransform, Transform rotateObject,
            IPlayerLookSettings lookSettings, ICoroutineStarter coroutineStarter)
        {
            _cameraRootTransform = cameraRootTransform;
            _rotateObject = rotateObject;
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
            _rotateObject.Rotate(Vector3.up, mouseLookDelta.x);
        }

        public void StartLookingAtPoint(Vector3 lookPoint)
        {
            _isCameraLockedOnPoint = true;
            _coroutineStarter.StartCoroutine(LockAtHookPoint());
            // _cameraTransform.DOLookAt(lookPoint, _lookSettings.LookAtStartAnimationDuration)
            //                 .SetEase(_lookSettings.LookAtStartAnimationEase)
            //                 .SetLink(_linkGameObject)
            //                 .OnComplete(() =>
            //                 {
            //                     _coroutineStarter.StartCoroutine(LockAtHookPoint());
            //                 });
        }

        public void StopLookingAtPoint()
        {
            _isCameraLockedOnPoint = false;
            _cameraTransform.DOKill();
        }

        private IEnumerator LockAtHookPoint() // TODO: implement this
        {
            Quaternion cameraRotation, rotateObjectRotation;
            Vector3 lookDirection;
            while (_isCameraLockedOnPoint)
            {
                lookDirection = _lockPoint - _cameraTransform.position;
                cameraRotation = Quaternion.LookRotation(lookDirection, _cameraTransform.forward);
                cameraRotation.z = 0f;
                cameraRotation.x = 0f;
                cameraLookPoint.y = _rotateObject;
                                    
                _rotateObject.eulerAngles = Vector3.Slerp(_rotateObject.rotation, targetRotation, Time.deltaTime * _lookSettings.RotationSpeed);
                
                direction.y = 0;
                Quaternion yRotation = Quaternion.LookRotation(direction);
                
                _cameraTransform.eulerAngles = Vector3.Slerp(_cameraTransform.rotation, yRotation, Time.deltaTime * _lookSettings.);
                yield return null;
            }
        }
        
        
    }
}