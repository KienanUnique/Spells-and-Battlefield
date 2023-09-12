using System;
using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Interfaces;
using UnityEngine;

namespace Enemies.Trigger
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyTargetTrigger : BoxColliderTriggerBase<IEnemyTarget>, IEnemyTargetTrigger
    {
        public event Action<IEnemyTarget> TargetDetected;
        public event Action<IEnemyTarget> TargetLost;

        public IReadOnlyList<IEnemyTarget> TargetsInTrigger => _requiredObjectsInside;

        public bool IsTargetInTrigger(IEnemyTarget target)
        {
            return _requiredObjectsInside.Contains(target);
        }

        public void ForgetTarget(IEnemyTarget targetToForget)
        {
            _requiredObjectsInside.Remove(targetToForget);
        }

        private void OnEnable()
        {
            RequiredObjectEnteringDetected += OnRequiredObjectEnteringDetected;
            RequiredObjectExitingDetected += OnRequiredObjectExitingDetected;
        }

        private void OnDisable()
        {
            RequiredObjectEnteringDetected -= OnRequiredObjectEnteringDetected;
            RequiredObjectExitingDetected -= OnRequiredObjectExitingDetected;
        }

        private void OnRequiredObjectEnteringDetected(IEnemyTarget obj)
        {
            TargetDetected?.Invoke(obj);
        }

        private void OnRequiredObjectExitingDetected(IEnemyTarget obj)
        {
            TargetLost?.Invoke(obj);
        }
    }
}