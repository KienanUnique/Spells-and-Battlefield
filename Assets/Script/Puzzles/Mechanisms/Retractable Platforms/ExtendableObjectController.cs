using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using DG.Tweening;
using Puzzles.Mechanisms_Triggers;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Retractable_Platforms
{
    public class ExtendableObjectController : InitializableMonoBehaviourBase,
        IInitializableExtendableObjectController
    {
        private const float PulledInScaleZ = 0f;
        private List<IMechanismsTrigger> _triggers;
        private Vector3 _pulledInPosition;
        private float _pulledOutScaleZ;
        private Vector3 _pulledOutPosition;
        private Transform _objectToExtend;
        private ExtendableObjectsSettings _settings;
        private float _animationDuration;
        private ValueWithReactionOnChange<ExtendableObjectState> _currentState;

        public void Initialize(List<IMechanismsTrigger> triggers, ExtendableObjectState startState, Vector3 startPosition,
            Vector3 endPosition, float animationDuration, Transform objectToExtend, ExtendableObjectsSettings settings)
        {
            _objectToExtend = objectToExtend;
            _triggers = triggers;
            _pulledOutScaleZ = Vector3.Distance(endPosition, startPosition);
            _pulledOutPosition = (endPosition + startPosition) / 2;
            _pulledInPosition = startPosition;
            _objectToExtend.rotation = Quaternion.LookRotation(_pulledOutPosition - _pulledInPosition);
            _animationDuration = animationDuration;
            _settings = settings;
            _currentState = new ValueWithReactionOnChange<ExtendableObjectState>(startState);
            var localScale = _objectToExtend.localScale;
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

            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.Triggered += OnTriggered;
            }

            _currentState.AfterValueChanged += OnPlatformStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _triggers)
            {
                trigger.Triggered -= OnTriggered;
            }

            _currentState.AfterValueChanged -= OnPlatformStateChanged;
        }

        private void OnTriggered()
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
            _objectToExtend.DOComplete();
            _objectToExtend.DOScaleZ(targetScaleZ, _animationDuration).SetEase(_settings.AnimationEase).SetLink(gameObject);
            _objectToExtend.DOMove(targetPosition, _animationDuration).SetEase(_settings.AnimationEase)
                .SetLink(gameObject);
        }
    }
}