﻿using System;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class DistanceTransitionEnemyAI : TransitionEnemyAI
    {
        [SerializeField] private float _transitionDistance;
        [SerializeField] private TypeOfComparison _typeOfComparison;
        private Transform _localTransform;

        protected override void CheckConditions()
        {
            var calculatedDistance = Vector3.Distance(_localTransform.position,
                StateMachineControllable.Target.MainTransform.position);
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

        private void Awake()
        {
            _localTransform = transform;
        }

        private enum TypeOfComparison
        {
            IsMore,
            IsLess
        }
    }
}