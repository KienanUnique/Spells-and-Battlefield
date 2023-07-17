﻿using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public abstract class MovingPlatformWithStickingBase : InitializableMonoBehaviourBase
    {
        protected Transform _parentObjectToMove;
        protected MovingPlatformsSettings _settings;
        protected float _movementSpeed;
        protected List<Vector3> _waypoints;
        protected bool _isTriggersDisabled;
        protected float _delayInSeconds;
        private IColliderTrigger _platformCollider;

        protected void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _parentObjectToMove = dataForControllerBase.ObjectToMove;
            _platformCollider = dataForControllerBase.PlatformCollider;
            _settings = dataForControllerBase.Settings;
            _movementSpeed = dataForControllerBase.MovementSpeed;
            _waypoints = dataForControllerBase.Waypoints;
            _isTriggersDisabled = false;
            _delayInSeconds = dataForControllerBase.DelayInSeconds;
            _parentObjectToMove.position = _waypoints.First();
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            _platformCollider.TriggerEnter += OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit += OnPlatformColliderTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            _platformCollider.TriggerEnter -= OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit -= OnPlatformColliderTriggerExit;
        }

        private void OnPlatformColliderTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IToMovingPlatformStickable>(out var stickable)) return;
            stickable.StickToPlatform(_parentObjectToMove);
        }

        private void OnPlatformColliderTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IToMovingPlatformStickable>(out var stickable)) return;
            stickable.UnstickFromPlatform();
        }
    }
}