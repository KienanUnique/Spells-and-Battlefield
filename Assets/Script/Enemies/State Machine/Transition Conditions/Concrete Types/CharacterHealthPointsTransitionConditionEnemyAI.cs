using System;
using Common;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class CharacterHealthPointsTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] [Range(0.001f, 1f)] private float _needRatioOfHealthPoints;
        [SerializeField] private TypeOfComparison _typeOfComparison;

        public override bool IsConditionCompleted
        {
            get
            {
                switch (_typeOfComparison)
                {
                    case TypeOfComparison.IsMore:
                        if (StateMachineControllable.HitPointCountRatio > _needRatioOfHealthPoints)
                        {
                            return true;
                        }

                        break;
                    case TypeOfComparison.IsLess:
                        if (StateMachineControllable.HitPointCountRatio < _needRatioOfHealthPoints)
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

        protected override void HandleStartCheckingConditions()
        {
            SubscribeOnThisCharacterEvents();
        }

        protected override void HandleStopCheckingConditions()
        {
            UnsubscribeFromThisCharacterEvents();
        }

        protected override void SubscribeOnEvents()
        {
            if (!IsEnabled) return;
            SubscribeOnThisCharacterEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromThisCharacterEvents();
        }

        private void SubscribeOnThisCharacterEvents()
        {
            StateMachineControllable.HitPointsCountChanged += OnHitPointsCountChanged;
        }

        private void UnsubscribeFromThisCharacterEvents()
        {
            StateMachineControllable.HitPointsCountChanged -= OnHitPointsCountChanged;
        }

        private void OnHitPointsCountChanged(float obj)
        {
            if (IsConditionCompleted)
            {
                InvokeConditionCompletedEvent();
            }
        }
    }
}