using System.Collections;
using Common.Abstract_Bases.Disableable;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Look_Point_Calculator;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using UnityEngine;

namespace Enemies.Look
{
    public class EnemyLook : BaseWithDisabling, IEnemyLook
    {
        private readonly Transform _transformToRotate;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly IReadonlyRigidbody _thisRigidbody;
        private readonly IReadonlyEnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private readonly Transform _transformToRotateForIK;
        private readonly IReadonlyTransform _thisIKCenterPoint;
        private readonly float _needDistanceFromIKCenterPoint;
        private Vector3 _cachedLookXZ;
        private IReadonlyTransform _thisPositionReferencePoint;
        private ILookPointCalculator _lookPointCalculator;
        private Coroutine _lookCoroutine;
        private Vector3 _needDirection;

        public EnemyLook(Transform transformToRotate, IReadonlyRigidbody thisRigidbody,
            IReadonlyTransform thisPositionReferencePoint,
            IReadonlyEnemyTargetFromTriggersSelector targetFromTriggersSelector, ICoroutineStarter coroutineStarter,
            Transform transformToRotateForIK, IReadonlyTransform thisIKCenterPoint, float needDistanceFromIKCenterPoint)
        {
            _transformToRotate = transformToRotate;
            _coroutineStarter = coroutineStarter;
            _thisRigidbody = thisRigidbody;
            _thisPositionReferencePoint = thisPositionReferencePoint;
            _targetFromTriggersSelector = targetFromTriggersSelector;
            _transformToRotateForIK = transformToRotateForIK;
            _thisIKCenterPoint = thisIKCenterPoint;
            _cachedLookXZ = Vector3.zero;
            _needDistanceFromIKCenterPoint = needDistanceFromIKCenterPoint;
        }

        public IReadonlyTransform ThisPositionReferencePointForLook => _thisPositionReferencePoint;

        protected override void SubscribeOnEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
        }

        public void StartLooking()
        {
            TryStartLookingCoroutine();
        }

        public void SetLookPointCalculator(ILookPointCalculator lookPointCalculator)
        {
            if (lookPointCalculator == null) return;

            lookPointCalculator.SetLookData(_thisRigidbody, _thisPositionReferencePoint,
                _targetFromTriggersSelector.CurrentTarget);
            _lookPointCalculator = lookPointCalculator;
            TryStartLookingCoroutine();
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            _thisPositionReferencePoint = newReferenceTransform;
            _lookPointCalculator.ChangeThisPositionReferencePointTransform(newReferenceTransform);
        }

        public void StopLooking()
        {
            if (_lookCoroutine == null) return;
            _coroutineStarter.StopCoroutine(_lookCoroutine);
        }

        private void TryStartLookingCoroutine()
        {
            if (_lookCoroutine != null || _lookPointCalculator == null) return;
            _lookCoroutine = _coroutineStarter.StartCoroutine(LookUsingCalculator());
        }

        private IEnumerator LookUsingCalculator()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                HandleLookPoint(_lookPointCalculator.CalculateLookPointDirection());
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

            _needDirection = Vector3.Reflect(lookRotation, _thisIKCenterPoint.Up);
            _transformToRotateForIK.position =
                _needDistanceFromIKCenterPoint * _needDirection + _thisIKCenterPoint.Position;
        }

        private void OnCurrentTargetChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            _lookPointCalculator.UpdateTarget(newTarget);
        }
    }
}