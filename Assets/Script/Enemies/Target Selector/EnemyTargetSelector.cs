using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Enemies.Trigger;
using Interfaces;
using ModestTree;

namespace Enemies.Target_Selector
{
    public class EnemyTargetSelector : BaseWithDisabling, IEnemyTargetSelector
    {
        private readonly HashSet<IEnemyTarget> _targets = new HashSet<IEnemyTarget>();
        private readonly List<IEnemyTargetTrigger> _triggers = new List<IEnemyTargetTrigger>();
        public event Action<IEnemyTarget> CurrentTargetChanged;

        public IEnemyTarget CurrentTarget { get; private set; }

        public void AddTrigger(IEnemyTargetTrigger trigger)
        {
            _triggers.Add(trigger);
            trigger.TargetDetected += OnTargetInTriggerDetected;
            trigger.TargetLost += OnTargetInTriggerLost;
        }

        public void SwitchToNextTarget()
        {
            RemoveTargetIfUnreachable(CurrentTarget);
        }

        protected sealed override void SubscribeOnEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.TargetDetected += OnTargetInTriggerDetected;
                trigger.TargetLost += OnTargetInTriggerLost;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.TargetDetected -= OnTargetInTriggerDetected;
                trigger.TargetLost -= OnTargetInTriggerLost;
            }
        }

        private void OnTargetInTriggerDetected(IEnemyTarget target)
        {
            if (_targets.IsEmpty())
            {
                CurrentTarget = target;
                CurrentTargetChanged?.Invoke(CurrentTarget);
            }

            _targets.Add(target);
        }

        private void OnTargetInTriggerLost(IEnemyTarget target)
        {
            RemoveTargetIfUnreachable(target);
        }

        private void RemoveTargetIfUnreachable(IEnemyTarget target)
        {
            if (!_triggers.Any(trigger => trigger.IsTargetInTrigger(target)))
            {
                _targets.Remove(target);

                if (target == CurrentTarget)
                {
                    CurrentTarget = _targets.IsEmpty() ? null : _targets.First();
                    CurrentTargetChanged?.Invoke(CurrentTarget);
                }
            }
        }
    }
}