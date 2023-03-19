using System;
using UnityEngine;

namespace Enemies.State_Machine.Transitions
{
    public class DistanceTransition : Transition
    {
        [SerializeField] private float _transitionDistance;
        [SerializeField] private TypeOfComparison _typeOfComparison;
        private Transform _localTransform;

        protected override void CheckConditions()
        {
            var calculatedDistance = Vector3.Distance(_localTransform.position, Target.MainTransform.position);
            switch (_typeOfComparison)
            {
                case TypeOfComparison.IsMore:
                    if (calculatedDistance > _transitionDistance)
                    {
                        NeedTransit = true;
                    }

                    break;
                case TypeOfComparison.IsLess:
                    if (calculatedDistance < _transitionDistance)
                    {
                        NeedTransit = true;
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