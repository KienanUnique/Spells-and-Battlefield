using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class DistanceTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] private float _transitionDistance;
        [SerializeField] private TypeOfComparison _typeOfComparison;
        private Transform _cashedTransform;
        private Coroutine _currentCheckConditionsCoroutine;
        private ValueWithReactionOnChange<bool> _isConditionCompleted;

        public override bool IsConditionCompleted => _isConditionCompleted.Value;

        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;
        private Vector3 CurrentTargetPosition => CurrentTarget.MainRigidbody.Position;

        protected override void Awake()
        {
            base.Awake();
            _cashedTransform = transform;
            _isConditionCompleted = new ValueWithReactionOnChange<bool>(false);
        }

        protected override void HandleStartCheckingConditions()
        {
            if (_currentCheckConditionsCoroutine != null)
            {
                return;
            }

            _currentCheckConditionsCoroutine = StartCoroutine(CheckConditionsCoroutine());
        }

        protected override void HandleStopCheckingConditions()
        {
            _isConditionCompleted.Value = false;
            if (_currentCheckConditionsCoroutine == null)
            {
                return;
            }

            StopCoroutine(_currentCheckConditionsCoroutine);

            _currentCheckConditionsCoroutine = null;
        }

        protected override void SubscribeOnEvents()
        {
            _isConditionCompleted.AfterValueChanged += OnConditionCompletedStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _isConditionCompleted.AfterValueChanged -= OnConditionCompletedStateChanged;
        }

        private void OnConditionCompletedStateChanged(bool newIsConditionCompleted)
        {
            if (newIsConditionCompleted)
            {
                InvokeConditionCompletedEvent();
            }
        }

        private IEnumerator CheckConditionsCoroutine()
        {
            while (true)
            {
                _isConditionCompleted.Value = CheckConditions();
                yield return null;
            }
        }

        private bool CheckConditions()
        {
            if (CurrentTarget == null)
            {
                return false;
            }

            var calculatedDistance = Vector3.Distance(_cashedTransform.position, CurrentTargetPosition);
            switch (_typeOfComparison)
            {
                case TypeOfComparison.IsMore:
                    if (calculatedDistance > _transitionDistance)
                    {
                        return true;
                    }

                    break;
                case TypeOfComparison.IsLess:
                    if (calculatedDistance < _transitionDistance)
                    {
                        return true;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}