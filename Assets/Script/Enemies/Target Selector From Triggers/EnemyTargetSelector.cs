using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using Enemies.Trigger;
using Factions;
using Interfaces;
using UnityEngine;

namespace Enemies.Target_Selector_From_Triggers
{
    public class EnemyTargetFromTriggersSelector : BaseWithDisabling, IEnemyTargetFromTriggersSelector
    {
        private readonly IFaction _thisFaction;
        private readonly HashSet<IEnemyTarget> _waitingInitializationTargets = new HashSet<IEnemyTarget>();
        private readonly List<IEnemyTarget> _targets = new List<IEnemyTarget>();
        private readonly List<IEnemyTargetTrigger> _triggers = new List<IEnemyTargetTrigger>();
        private readonly IReadonlyTransform _thisTransform;
        private readonly float _targetSelectorUpdateCooldownInSeconds;

        public EnemyTargetFromTriggersSelector(IFaction thisFaction, IReadonlyTransform thisTransform,
            ICoroutineStarter coroutineStarter, float targetSelectorUpdateCooldownInSeconds)
        {
            _thisFaction = thisFaction;
            _thisTransform = thisTransform;
            _targetSelectorUpdateCooldownInSeconds = targetSelectorUpdateCooldownInSeconds;
            coroutineStarter.StartCoroutine(UpdateCurrentTargetCoroutine());
        }

        public event IReadonlyEnemyTargetFromTriggersSelector.CurrentTargetChangedEventHandler CurrentTargetChanged;
        public IEnemyTarget CurrentTarget { get; private set; }

        public void AddTrigger(IEnemyTargetTrigger trigger)
        {
            _triggers.Add(trigger);
            foreach (IEnemyTarget enemyTarget in trigger.TargetsInTrigger)
            {
                OnTargetDetected(enemyTarget);
            }

            trigger.TargetDetected += OnTargetDetected;
            trigger.TargetLost += OnTargetInTriggerLost;
        }

        public void AddTriggers(IEnumerable<IEnemyTargetTrigger> triggers)
        {
            foreach (IEnemyTargetTrigger trigger in triggers)
            {
                AddTrigger(trigger);
            }
        }

        protected sealed override void SubscribeOnEvents()
        {
            foreach (IEnemyTargetTrigger trigger in _triggers)
            {
                trigger.TargetDetected += OnTargetDetected;
                trigger.TargetLost += OnTargetInTriggerLost;
            }

            foreach (IEnemyTarget target in _targets)
            {
                SubscribeOnInitializedTarget(target);
            }

            foreach (IEnemyTarget target in _waitingInitializationTargets)
            {
                SubscribeOnNonInitializedTarget(target);
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IEnemyTargetTrigger trigger in _triggers)
            {
                trigger.TargetDetected -= OnTargetDetected;
                trigger.TargetLost -= OnTargetInTriggerLost;
            }

            foreach (IEnemyTarget target in _targets)
            {
                UnsubscribeFromInitializedTarget(target);
            }

            foreach (IEnemyTarget target in _waitingInitializationTargets)
            {
                UnsubscribeFromNonInitializedTarget(target);
            }
        }

        private void OnTargetDetected(IEnemyTarget newTarget)
        {
            if (_targets.Contains(newTarget))
            {
                return;
            }

            switch (newTarget.CurrentInitializationStatus)
            {
                case InitializationStatus.NonInitialized:
                    _waitingInitializationTargets.Add(newTarget);
                    if (IsEnabled)
                    {
                        SubscribeOnNonInitializedTarget(newTarget);
                    }

                    break;
                case InitializationStatus.Initialized:
                    HandleNewDetectedTarget(newTarget);
                    break;
            }
        }

        private void OnTargetInitializationStatusChanged(InitializationStatus newInitializationStatus)
        {
            if (newInitializationStatus == InitializationStatus.NonInitialized)
            {
                return;
            }

            bool IsTargetInitialized(IEnemyTarget target) =>
                target.CurrentInitializationStatus == InitializationStatus.Initialized;

            foreach (IEnemyTarget enemyTarget in _waitingInitializationTargets.Where(IsTargetInitialized))
            {
                UnsubscribeFromNonInitializedTarget(enemyTarget);
                HandleNewDetectedTarget(enemyTarget);
            }

            _waitingInitializationTargets.RemoveWhere(IsTargetInitialized);
        }

        private void SubscribeOnNonInitializedTarget(IInitializable target)
        {
            target.InitializationStatusChanged += OnTargetInitializationStatusChanged;
        }

        private void UnsubscribeFromNonInitializedTarget(IInitializable target)
        {
            target.InitializationStatusChanged -= OnTargetInitializationStatusChanged;
        }

        private void SubscribeOnInitializedTarget(IEnemyTarget target)
        {
            target.Destroying += OnTargetStartDestroying;
            target.CharacterStateChanged += OnTargetStateChanged;
        }

        private void UnsubscribeFromInitializedTarget(IEnemyTarget target)
        {
            target.Destroying -= OnTargetStartDestroying;
            target.CharacterStateChanged -= OnTargetStateChanged;
        }

        private void OnTargetStateChanged(CharacterState newState)
        {
            if (newState != CharacterState.Dead)
            {
                return;
            }

            bool IsTargetDead(IEnemyTarget target) => target.CurrentCharacterState == CharacterState.Dead;

            var targetsToRemove = new List<IEnemyTarget>(_targets.Where(IsTargetDead));
            foreach (IEnemyTarget enemyTarget in targetsToRemove)
            {
                RemoveTarget(enemyTarget);
            }
        }

        private void OnTargetStartDestroying(IEnemyTarget destroyingTarget)
        {
            RemoveTarget(destroyingTarget);
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
                _thisFaction.GetRelationshipToOtherFraction(newTarget.Faction) ==
                OtherFactionRelationship.Friendly ||
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