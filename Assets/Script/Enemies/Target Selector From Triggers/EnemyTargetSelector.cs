﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Interfaces;
using ModestTree;

namespace Enemies.Target_Selector_From_Triggers
{
    public class EnemyTargetFromTriggersSelector : BaseWithDisabling, IEnemyTargetFromTriggersSelector
    {
        private readonly HashSet<IEnemyTarget> _targets = new HashSet<IEnemyTarget>();
        private readonly List<Trigger.IEnemyTargetTrigger> _triggers = new List<Trigger.IEnemyTargetTrigger>();
        public event Action<IEnemyTarget> CurrentTargetChanged;

        public IEnemyTarget CurrentTarget { get; private set; }

        public void AddTrigger(Trigger.IEnemyTargetTrigger trigger)
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