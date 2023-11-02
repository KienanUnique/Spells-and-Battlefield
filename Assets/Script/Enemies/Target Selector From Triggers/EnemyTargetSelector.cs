using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Disableable;
using Common.Interfaces;
using Common.Readonly_Transform;
using Enemies.Trigger;
using Factions;
using UnityEngine;

namespace Enemies.Target_Selector_From_Triggers
{
    public class EnemyTargetFromTriggersSelector : BaseWithDisabling, IEnemyTargetFromTriggersSelector
    {
        private readonly IFaction _thisFaction;
        private readonly List<IEnemyTarget> _targets = new List<IEnemyTarget>();
        private readonly List<IEnemyTargetTrigger> _triggers = new List<IEnemyTargetTrigger>();
        private readonly IReadonlyTransform _thisTransform;
        private readonly float _targetSelectorUpdateCooldownInSeconds;
        private readonly ICoroutineStarter _coroutineStarter;
        private Coroutine _selectingTargetsCoroutine;

        public EnemyTargetFromTriggersSelector(IFaction thisFaction, IReadonlyTransform thisTransform,
            ICoroutineStarter coroutineStarter, float targetSelectorUpdateCooldownInSeconds)
        {
            _thisFaction = thisFaction;
            _thisTransform = thisTransform;
            _targetSelectorUpdateCooldownInSeconds = targetSelectorUpdateCooldownInSeconds;
            _coroutineStarter = coroutineStarter;
        }

        public event IReadonlyEnemyTargetFromTriggersSelector.CurrentTargetChangedEventHandler CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; private set; }
        private bool IsActive => _selectingTargetsCoroutine != null;

        public void AddTrigger(IEnemyTargetTrigger trigger)
        {
            _triggers.Add(trigger);
            foreach (IEnemyTarget enemyTarget in trigger.TargetsInTrigger)
            {
                OnTargetDetected(enemyTarget);
            }

            if (IsEnabled)
            {
                SubscribeOnTriggerEvents(trigger);
            }
        }

        public void AddTriggers(IEnumerable<IEnemyTargetTrigger> triggers)
        {
            foreach (IEnemyTargetTrigger trigger in triggers)
            {
                AddTrigger(trigger);
            }
        }

        public void StartSelecting()
        {
            _selectingTargetsCoroutine ??= _coroutineStarter.StartCoroutine(UpdateCurrentTargetCoroutine());
        }

        public void StopSelecting()
        {
            if (_selectingTargetsCoroutine == null)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_selectingTargetsCoroutine);
            _selectingTargetsCoroutine = null;
        }

        protected sealed override void SubscribeOnEvents()
        {
            foreach (IEnemyTargetTrigger trigger in _triggers)
            {
                SubscribeOnTriggerEvents(trigger);
            }

            foreach (IEnemyTarget target in _targets)
            {
                SubscribeOnInitializedTarget(target);
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IEnemyTargetTrigger trigger in _triggers)
            {
                UnsubscribeFromTriggerEvents(trigger);
            }

            foreach (IEnemyTarget target in _targets)
            {
                UnsubscribeFromInitializedTarget(target);
            }
        }

        private void SubscribeOnTriggerEvents(IEnemyTargetTrigger trigger)
        {
            trigger.TargetDetected += OnTargetDetected;
            trigger.TargetLost += OnTargetInTriggerLost;
        }

        private void UnsubscribeFromTriggerEvents(IEnemyTargetTrigger trigger)
        {
            trigger.TargetDetected -= OnTargetDetected;
            trigger.TargetLost -= OnTargetInTriggerLost;
        }

        private void OnTargetDetected(IEnemyTarget newTarget)
        {
            if (_targets.Contains(newTarget))
            {
                return;
            }

            HandleNewDetectedTarget(newTarget);
        }

        private void SubscribeOnInitializedTarget(IEnemyTarget target)
        {
            target.CharacterStateChanged += OnTargetStateChanged;
        }

        private void UnsubscribeFromInitializedTarget(IEnemyTarget target)
        {
            target.CharacterStateChanged -= OnTargetStateChanged;
        }

        private void OnTargetStateChanged(CharacterState newState)
        {
            if (newState != CharacterState.Dead)
            {
                return;
            }

            bool IsTargetDead(IEnemyTarget target)
            {
                return target.CurrentCharacterState == CharacterState.Dead;
            }

            var targetsToRemove = new List<IEnemyTarget>(_targets.Where(IsTargetDead));
            foreach (IEnemyTarget enemyTarget in targetsToRemove)
            {
                RemoveTarget(enemyTarget);
            }
        }

        private IEnumerator UpdateCurrentTargetCoroutine()
        {
            var waitForCooldown = new WaitForSeconds(_targetSelectorUpdateCooldownInSeconds);
            while (true)
            {
                UpdateSelectedTarget();
                yield return waitForCooldown;
            }
        }

        private void HandleNewDetectedTarget(IEnemyTarget newTarget)
        {
            if (_targets.Contains(newTarget) ||
                _thisFaction.GetRelationshipToOtherFraction(newTarget.Faction) == OtherFactionRelationship.Friendly ||
                newTarget.CurrentCharacterState == CharacterState.Dead)
            {
                return;
            }

            _targets.Add(newTarget);

            if (IsEnabled)
            {
                SubscribeOnInitializedTarget(newTarget);
            }

            UpdateSelectedTarget();
        }

        private void OnTargetInTriggerLost(IEnemyTarget target)
        {
            RemoveTargetIfUnreachable(target);
        }

        private void RemoveTargetIfUnreachable(IEnemyTarget target)
        {
            if (_triggers.Any(trigger => trigger.IsTargetInTrigger(target)))
            {
                return;
            }

            RemoveTarget(target);
        }

        private void RemoveTarget(IEnemyTarget targetToRemove)
        {
            UnsubscribeFromInitializedTarget(targetToRemove);

            foreach (IEnemyTargetTrigger trigger in _triggers)
            {
                trigger.ForgetTarget(targetToRemove);
            }

            _targets.Remove(targetToRemove);

            if (CurrentTarget == null || targetToRemove == CurrentTarget)
            {
                UpdateSelectedTarget();
            }
        }

        private void UpdateSelectedTarget()
        {
            if (!IsActive)
            {
                return;
            }

            IEnemyTarget nextTarget = _targets.Count switch
            {
                0 => null,
                1 => _targets.First(),
                _ => GetClosestTarget()
            };

            if (nextTarget == CurrentTarget)
            {
                return;
            }

            IEnemyTarget oldTarget = CurrentTarget;
            CurrentTarget = nextTarget;
            CurrentTargetChanged?.Invoke(oldTarget, nextTarget);
        }

        private IEnemyTarget GetClosestTarget()
        {
            IEnemyTarget closestTarget = _targets[0];
            float closestDistance = CalculateDistanceToTarget(closestTarget);
            for (var i = 1; i < _targets.Count; i++)
            {
                float tmpDistance = CalculateDistanceToTarget(_targets[i]);
                if (tmpDistance < closestDistance)
                {
                    closestDistance = tmpDistance;
                    closestTarget = _targets[i];
                }
            }

            return closestTarget;
        }

        private float CalculateDistanceToTarget(IEnemyTarget target)
        {
            return Vector3.Distance(_thisTransform.Position, target.PointForAiming.Position);
        }
    }
}