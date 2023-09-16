using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Box_Collider_Trigger;
using UnityEngine;

namespace Enemies.Trigger
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyTargetTrigger : TriggerForInitializableObjectsBase<IEnemyTarget>, IEnemyTargetTrigger
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

        protected override void OnEnable()
        {
            base.OnEnable();
            RequiredObjectEnteringDetected += OnTargetEnteringDetected;
            RequiredObjectExitingDetected += OnTargetExitingDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RequiredObjectEnteringDetected -= OnTargetEnteringDetected;
            RequiredObjectExitingDetected -= OnTargetExitingDetected;
        }

        private void OnTargetEnteringDetected(IEnemyTarget obj)
        {
            TargetDetected?.Invoke(obj);
        }

        private void OnTargetExitingDetected(IEnemyTarget obj)
        {
            TargetLost?.Invoke(obj);
        }
    }
}