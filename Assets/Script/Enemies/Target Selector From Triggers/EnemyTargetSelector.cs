using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Interfaces;
using ModestTree;

namespace Enemies.Target_Selector_From_Triggers
{
    public class EnemyTargetFromTriggersSelector : BaseWithDisabling, IEnemyTargetFromTriggersSelector
    {
        private readonly HashSet<IEnemyTarget> _targets = new HashSet<IEnemyTarget>();
        private readonly List<Trigger.IEnemyTargetTrigger> _triggers = new List<Trigger.IEnemyTargetTrigger>();
        public event IReadonlyEnemyTargetFromTriggersSelector.CurrentTargetChangedEventHandler CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; private set; }

        public void AddTrigger(Trigger.IEnemyTargetTrigger trigger)
        {
            _triggers.Add(trigger);
            foreach (var enemyTarget in trigger.TargetsInTrigger)
            {
                HandleNewDetectedTarget(enemyTarget);
            }

            trigger.TargetDetected += HandleNewDetectedTarget;
            trigger.TargetLost += OnTargetInTriggerLost;
        }

        public void AddTriggers(IEnumerable<Trigger.IEnemyTargetTrigger> triggers)
        {
            foreach (var trigger in triggers)
            {
                AddTrigger(trigger);
            }
        }

        protected sealed override void SubscribeOnEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.TargetDetected += HandleNewDetectedTarget;
                trigger.TargetLost += OnTargetInTriggerLost;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.TargetDetected -= HandleNewDetectedTarget;
                trigger.TargetLost -= OnTargetInTriggerLost;
            }
        }

        private void HandleNewDetectedTarget(IEnemyTarget newTarget)
        {
            if (_targets.IsEmpty())
            {
                var oldTarget = CurrentTarget;
                CurrentTarget = newTarget;
                CurrentTargetChanged?.Invoke(oldTarget, newTarget);
            }

            _targets.Add(newTarget);
        }

        private void OnTargetInTriggerLost(IEnemyTarget target)
        {
            RemoveTargetIfUnreachable(target);
        }

        private void RemoveTargetIfUnreachable(IEnemyTarget target)
        {
            if (_triggers.Any(trigger => trigger.IsTargetInTrigger(target))) return;
            _targets.Remove(target);

            if (target == CurrentTarget)
            {
                var nextTarget = _targets.IsEmpty() ? null : _targets.First();
                CurrentTargetChanged?.Invoke(CurrentTarget, nextTarget);
            }
        }
    }
}