using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using Puzzles.Mechanisms.Extendable_Object.Settings;
using Puzzles.Mechanisms.Extendable_Object.Setup;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Extendable_Object
{
    public class ExtendableObjectController : MechanismControllerBase, IInitializableExtendableObjectController
    {
        private const float PulledInScaleZ = 0f;
        private float _animationDuration;
        private ValueWithReactionOnChange<ExtendableObjectState> _currentState;
        private Transform _objectToExtend;
        private Vector3 _pulledInPosition;
        private Vector3 _pulledOutPosition;
        private float _pulledOutScaleZ;
        private IExtendableObjectsSettings _settings;
        private Sequence _sequence;

        public void Initialize(List<IMechanismsTrigger> triggers, ExtendableObjectState startState,
            Vector3 startPosition, Vector3 endPosition, float animationDuration, Transform objectToExtend,
            IExtendableObjectsSettings settings)
        {
            _objectToExtend = objectToExtend;
            _pulledOutScaleZ = Vector3.Distance(endPosition, startPosition);
            _pulledOutPosition = (endPosition + startPosition) / 2;
            _pulledInPosition = startPosition;
            _objectToExtend.rotation = Quaternion.LookRotation(_pulledOutPosition - _pulledInPosition);
            _animationDuration = animationDuration;
            _settings = settings;
            _currentState = new ValueWithReactionOnChange<ExtendableObjectState>(startState);
            Vector3 localScale = _objectToExtend.localScale;
            switch (startState)
            {
                case ExtendableObjectState.PulledIn:
                    _objectToExtend.localScale = new Vector3(localScale.x, localScale.y, PulledInScaleZ);
                    _objectToExtend.position = _pulledInPosition;
                    break;
                case ExtendableObjectState.PulledOut:
                    _objectToExtend.localScale = new Vector3(localScale.x, localScale.y, _pulledOutScaleZ);
                    _objectToExtend.position = _pulledOutPosition;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(startState), startState, null);
            }

            AddTriggers(triggers);

            SetInitializedStatus();
        }

        protected override void StartJob()
        {
            switch (_currentState.Value)
            {
                case ExtendableObjectState.PulledIn:
                    _currentState.Value = ExtendableObjectState.PulledOut;
                    break;
                case ExtendableObjectState.PulledOut:
                    _currentState.Value = ExtendableObjectState.PulledIn;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _currentState.AfterValueChanged += OnPlatformStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _currentState.AfterValueChanged -= OnPlatformStateChanged;
        }

        private void OnPlatformStateChanged(ExtendableObjectState newState)
        {
            switch (_currentState.Value)
            {
                case ExtendableObjectState.PulledIn:
                    MoveToState(PulledInScaleZ, _pulledInPosition);
                    break;
                case ExtendableObjectState.PulledOut:
                    MoveToState(_pulledOutScaleZ, _pulledOutPosition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveToState(float targetScaleZ, Vector3 targetPosition)
        {
            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();

            _sequence.Append(_objectToExtend.DOScaleZ(targetScaleZ, _animationDuration))
                     .Join(_objectToExtend.DOMove(targetPosition, _animationDuration))
                     .SetEase(_settings.AnimationEase)
                     .SetLink(gameObject)
                     .OnComplete(HandleDoneJob);
        }
    }
}