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
        private readonly IFactionHolder _factionHolder;
        private readonly List<IEnemyTarget> _aggressiveTargets = new();
        private readonly List<IEnemyTarget> _allTargets = new();
        private readonly List<IEnemyTargetTrigger> _triggers = new();
        private readonly IReadonlyTransform _thisTransform;
        private readonly float _targetSelectorUpdateCooldownInSeconds;
        private readonly ICoroutineStarter _coroutineStarter;
        private Coroutine _selectingTargetsCoroutine;

        public EnemyTargetFromTriggersSelector(IFactionHolder factionHolder, IReadonlyTransform thisTransform,
            ICoroutineStarter coroutineStarter, float targetSelectorUpdateCooldownInSeconds)
        {
            _factionHolder = factionHolder;
            _thisTransform = thisTransform;
            _targetSelectorUpdateCooldownInSeconds = targetSelectorUpdateCooldownInSeconds;
            _coroutineStarter = coroutineStarter;
        }

        public event IReadonlyEnemyTargetFromTriggersSelector.CurrentTargetChangedEventHandler CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; private set; }
        private bool IsActive => _selectingTargetsCoroutine != null;
        private IFaction CurrentFaction => _factionHolder.Faction;

        public void AddTrigger(IEnemyTargetTrigger trigger)
        {
            _triggers.Add(trigger);
            foreach (var enemyTarget in trigger.TargetsInTrigger)
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
            foreach (var trigger in triggers)
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

        public void ResetTargets()
        {
            _aggressiveTargets.Clear();
            foreach (var target in _allTargets)
            {
                if (IsFriendly(target))
                {
                    continue;
                }
                _aggressiveTargets.Add(target);
            }
            UpdateSelectedTarget();
        }
        

        protected sealed override void SubscribeOnEvents()
        {
            foreach (var trigger in _triggers)
            {
                SubscribeOnTriggerEvents(trigger);
            }

            foreach (var target in _allTargets)
            {
                SubscribeOnInitializedTarget(target);
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _triggers)
            {
                UnsubscribeFromTriggerEvents(trigger);
            }

            foreach (var target in _allTargets)
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
            if (_allTargets.Contains(newTarget))
            {
                return;
            }

            HandleNewDetectedTarget(newTarget);
        }

        private void SubscribeOnInitializedTarget(IEnemyTarget target)
        {
            target.CharacterStateChanged += OnTargetStateChanged;
            target.FactionChanged += OnFactionChanged;
        }

        private void UnsubscribeFromInitializedTarget(IEnemyTarget target)
        {
            target.CharacterStateChanged -= OnTargetStateChanged;
            target.FactionChanged -= OnFactionChanged;
        }

        private void OnFactionChanged(IFaction newFaction)
        {
            foreach (var target in _aggressiveTargets)
            {
                if (!IsFriendly(target))
                {
                    continue;
                }

                _aggressiveTargets.Remove(target);
            }
            
            foreach (var target in _allTargets)
            {
                if (IsFriendly(target))
                {
                    continue;
                }

                _aggressiveTargets.Add(target);
            }
            
            UpdateSelectedTarget();
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

            var targetsToRemove = new List<IEnemyTarget>(_allTargets.Where(IsTargetDead));
            foreach (var enemyTarget in targetsToRemove)
            {
                RemoveTarget(enemyTarget);
            }
        }
        
        private bool IsFriendly(IEnemyTarget target)
        {
            return CurrentFaction.GetRelationshipToOtherFraction(target.Faction) == OtherFactionRelationship.Friendly;
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
            if (newTarget.CurrentCharacterState == CharacterState.Dead)
            {
                return;
            }

            _allTargets.Add(newTarget);
            if (IsEnabled)
            {
                SubscribeOnInitializedTarget(newTarget);
            }

            if (IsFriendly(newTarget))
            {
                return;
            }

            _aggressiveTargets.Add(newTarget);

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

            foreach (var trigger in _triggers)
            {
                trigger.ForgetTarget(targetToRemove);
            }

            _allTargets.Remove(targetToRemove);

            if (!_aggressiveTargets.Contains(targetToRemove))
            {
                return;
            }

            _aggressiveTargets.Remove(targetToRemove);
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

            var nextTarget = _aggressiveTargets.Count switch
            {
                0 => null,
                1 => _aggressiveTargets.First(),
                _ => GetClosestTarget()
            };

            if (nextTarget == CurrentTarget)
            {
                return;
            }

            var oldTarget = CurrentTarget;
            CurrentTarget = nextTarget;
            CurrentTargetChanged?.Invoke(oldTarget, nextTarget);
        }

        private IEnemyTarget GetClosestTarget()
        {
            var closestTarget = _aggressiveTargets[0];
            var closestDistance = CalculateDistanceToTarget(closestTarget);
            for (var i = 1; i < _aggressiveTargets.Count; i++)
            {
                var tmpDistance = CalculateDistanceToTarget(_aggressiveTargets[i]);
                if (tmpDistance < closestDistance)
                {
                    closestDistance = tmpDistance;
                    closestTarget = _aggressiveTargets[i];
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