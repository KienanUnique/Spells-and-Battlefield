using System.Collections;
using Common.Abstract_Bases.Disableable;
using Common.Readonly_Rigidbody;
using Enemies.Look_Point_Calculator;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using UnityEngine;

namespace Enemies.Look
{
    public class EnemyLook : IEnemyLook
    {
        private readonly Transform _transformToRotate;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly IReadonlyRigidbody _thisRigidbody;
        private readonly IReadonlyEnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private ILookPointCalculator _lookPointCalculator;
        private Coroutine _lookCoroutine;

        public EnemyLook(Transform transformToRotate, IReadonlyRigidbody thisRigidbody,
            IReadonlyEnemyTargetFromTriggersSelector targetFromTriggersSelector, ICoroutineStarter coroutineStarter)
        {
            _transformToRotate = transformToRotate;
            _coroutineStarter = coroutineStarter;
            _thisRigidbody = thisRigidbody;
            _targetFromTriggersSelector = targetFromTriggersSelector;
        }

        public void StartLooking()
        {
            TryStartLookingCoroutine();
        }

        public void SetLookPointCalculator(ILookPointCalculator lookPointCalculator)
        {
            if (lookPointCalculator == null) return;

            lookPointCalculator.SetLookData(_thisRigidbody, _targetFromTriggersSelector.CurrentTarget);
            _lookPointCalculator = lookPointCalculator;
            TryStartLookingCoroutine();
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
            while (true)
            {
                _transformToRotate.rotation = _lookPointCalculator.CalculateLookPointDirection();
                yield return null;
            }
        }
    }
}