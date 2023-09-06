using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using DG.Tweening;
using Puzzles.Mechanisms.Extendable_Object.Settings;
using Puzzles.Mechanisms.Extendable_Object.Setup;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Extendable_Object
{
    public class ExtendableObjectController : InitializableMonoBehaviourBase, IInitializableExtendableObjectController
    {
        private const float PulledInScaleZ = 0f;
        private float _animationDuration;
        private ValueWithReactionOnChange<ExtendableObjectState> _currentState;
        private Transform _objectToExtend;
        private Vector3 _pulledInPosition;
        private Vector3 _pulledOutPosition;
        private float _pulledOutScaleZ;
        private IExtendableObjectsSettings _settings;
        private List<IMechanismsTrigger> _triggers;

        public void Initialize(List<IMechanismsTrigger> triggers, ExtendableObjectState startState,
            Vector3 startPosition, Vector3 endPosition, float animationDuration, Transform objectToExtend,
            IExtendableObjectsSettings settings)
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

            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (IMechanismsTrigger trigger in _triggers)
            {
                trigger.Triggered += OnTriggered;
            }

            _currentState.AfterValueChanged += OnPlatformStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IMechanismsTrigger trigger in _triggers)
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
            _objectToExtend.DOScaleZ(targetScaleZ, _animationDuration)
                           .SetEase(_settings.AnimationEase)
                           .SetLink(gameObject);
            _objectToExtend.DOMove(targetPosition, _animationDuration)
                           .SetEase(_settings.AnimationEase)
                           .SetLink(gameObject);
        }
    }
}