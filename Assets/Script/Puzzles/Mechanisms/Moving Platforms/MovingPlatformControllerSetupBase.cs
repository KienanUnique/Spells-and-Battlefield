using System.Collections.Generic;
using Common.Abstract_Bases;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using UnityEngine;
using Zenject;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public abstract class MovingPlatformControllerSetupBase : SetupMonoBehaviourBase
    {
        [SerializeField] private Transform _objectToMove;
        [SerializeField] private List<Transform> _waypointTransforms;
        [SerializeField] private ColliderTrigger _platformColliderTrigger;
        [SerializeField] private float _movementSpeed = 16;
        private IMovingPlatformsSettings _settings;
        private List<Vector3> _waypoints;

        [Inject]
        private void GetDependencies(IMovingPlatformsSettings settings)
        {
            _settings = settings;
        }

        protected abstract void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase);

        protected sealed override void Initialize()
        {
            Initialize(new MovingPlatformDataForControllerBase(_settings, _movementSpeed, _waypoints, _objectToMove,
                _platformColliderTrigger));
        }

        protected override void Prepare()
        {
            _waypoints = new List<Vector3>();
            _waypointTransforms.ForEach(waypointTransform => _waypoints.Add(waypointTransform.position));
        }
    }
}