using System.Collections;
using Common.Abstract_Bases.Disableable;
using Common.Interfaces;
using Common.Look;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Look_Point_Calculator;
using Enemies.Target_Selector_From_Triggers;
using UnityEngine;

namespace Enemies.Look
{
    public class EnemyLook : BaseWithDisabling, IEnemyLook
    {
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly float _needDistanceFromIKCenterPoint;
        private readonly IReadonlyEnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private readonly IReadonlyTransform _thisIKCenterPoint;
        private readonly IReadonlyRigidbody _thisRigidbody;
        private readonly Transform _transformToRotate;
        private readonly Transform _transformToRotateForIK;
        private Vector3 _cachedLookXZ;
        private Coroutine _lookCoroutine;
        private ILookPointCalculator _lookPointCalculator;
        private RaycastHit _lookForwardRaycastHit;
        private ILookSettings _lookSettings;

        public EnemyLook(Transform transformToRotate, IReadonlyRigidbody thisRigidbody,
            IReadonlyTransform thisPositionReferencePoint,
            IReadonlyEnemyTargetFromTriggersSelector targetFromTriggersSelector, ICoroutineStarter coroutineStarter,
            Transform transformToRotateForIK, IReadonlyTransform thisIKCenterPoint, float needDistanceFromIKCenterPoint,
            ILookSettings lookSettings)
        {
            _transformToRotate = transformToRotate;
            _coroutineStarter = coroutineStarter;
            _thisRigidbody = thisRigidbody;
            ThisPositionReferencePointForLook = thisPositionReferencePoint;
            _targetFromTriggersSelector = targetFromTriggersSelector;
            _transformToRotateForIK = transformToRotateForIK;
            _thisIKCenterPoint = thisIKCenterPoint;
            _cachedLookXZ = Vector3.zero;
            _needDistanceFromIKCenterPoint = needDistanceFromIKCenterPoint;
        }

        public IReadonlyTransform ThisPositionReferencePointForLook { get; private set; }

        public Vector3 LookPointPosition
        {
            get
            {
                if (Physics.Raycast(ThisPositionReferencePointForLook.Position, LookDirection,
                        out _lookForwardRaycastHit, _lookSettings.MaxAimRaycastDistance, _lookSettings.AimLayerMask,
                        QueryTriggerInteraction.Ignore))
                {
                    return _lookForwardRaycastHit.point;
                }

                return ThisPositionReferencePointForLook.Position + LookDirection * _lookSettings.MaxAimRaycastDistance;
            }
        }

        public Vector3 LookDirection { get; private set; }

        public void StartLooking()
        {
            TryStartLookingCoroutine();
        }

        public void SetLookPointCalculator(ILookPointCalculator lookPointCalculator)
        {
            if (lookPointCalculator == null)
            {
                return;
            }

            lookPointCalculator.SetLookData(_thisRigidbody, ThisPositionReferencePointForLook,
                _targetFromTriggersSelector.CurrentTarget);
            _lookPointCalculator = lookPointCalculator;
            TryStartLookingCoroutine();
        }

        public void StopLooking()
        {
            if (_lookCoroutine == null)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_lookCoroutine);
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            ThisPositionReferencePointForLook = newReferenceTransform;
            _lookPointCalculator.ChangeThisPositionReferencePointTransform(newReferenceTransform);
        }

        protected override void SubscribeOnEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
        }

        private void TryStartLookingCoroutine()
        {
            if (_lookCoroutine != null || _lookPointCalculator == null)
            {
                return;
            }

            _lookCoroutine = _coroutineStarter.StartCoroutine(LookUsingCalculator());
        }

        private IEnumerator LookUsingCalculator()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                LookDirection = _lookPointCalculator.CalculateLookPointDirection();
                HandleLookPoint(LookDirection);
                yield return waitForFixedUpdate;
            }
        }

        private void HandleLookPoint(Vector3 lookRotation)
        {
            _cachedLookXZ.x = lookRotation.x;
            _cachedLookXZ.z = lookRotation.z;

            if (_cachedLookXZ != Vector3.zero)
            {
                _transformToRotate.rotation = Quaternion.LookRotation(_cachedLookXZ);
            }

            var needDirection = Vector3.Reflect(lookRotation, _thisIKCenterPoint.Up);
            _transformToRotateForIK.position =
                _needDistanceFromIKCenterPoint * needDirection + _thisIKCenterPoint.Position;
        }

        private void OnCurrentTargetChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            _lookPointCalculator.UpdateTarget(newTarget);
        }
    }
}