﻿using System;
using Common.Readonly_Transform;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transitions.Concrete_Types
{
    public class DistanceTransitionEnemyAI : TransitionEnemyAI
    {
        [SerializeField] private float _transitionDistance;
        [SerializeField] private TypeOfComparison _typeOfComparison;
        private Transform _cashedTransform;

        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;
        private Vector3 CurrentTargetPosition => ((IReadonlyTransform) CurrentTarget.MainRigidbody).Position;

        private enum TypeOfComparison
        {
            IsMore,
            IsLess
        }

        protected override void CheckConditions()
        {
            if (CurrentTarget == null)
            {
                return;
            }
            var calculatedDistance = Vector3.Distance(_cashedTransform.position, CurrentTargetPosition);
            switch (_typeOfComparison)
            {
                case TypeOfComparison.IsMore:
                    if (calculatedDistance > _transitionDistance)
                    {
                        InvokeTransitionEvent();
                    }

                    break;
                case TypeOfComparison.IsLess:
                    if (calculatedDistance < _transitionDistance)
                    {
                        InvokeTransitionEvent();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            _cashedTransform = transform;
        }
    }
}