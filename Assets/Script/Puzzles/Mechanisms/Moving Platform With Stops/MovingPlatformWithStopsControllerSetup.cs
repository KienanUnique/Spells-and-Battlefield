using System.Collections.Generic;
using Common.Abstract_Bases;
using Puzzles.Triggers;
using Puzzles.Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Moving_Platform_With_Stops
{
    public class MovingPlatformWithStopsControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private Transform _objectToMove;
        [SerializeField] private List<TriggerBase> _moveNextTriggers;
        [SerializeField] private List<TriggerBase> _movePreviousTriggers;
        [SerializeField] private List<Transform> _waypointTransforms;
        [SerializeField] private ColliderTrigger _platformColliderTrigger;
        [SerializeField] private float _movementSpeed = 16;
        private MovingPlatformWithStopsSettings _settings;
        private List<Vector3> _waypoints;
        private IInitializableMovingPlatformWithStopsController _controller;

        [Inject]
        private void Construct(MovingPlatformWithStopsSettings settings)
        {
            _settings = settings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization
        {
            get
            {
                var result = new List<IInitializable>();
                result.AddRange(_moveNextTriggers);
                result.AddRange(_movePreviousTriggers);
                return result;
            }
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableMovingPlatformWithStopsController>();
            _waypoints = new List<Vector3>();
            _waypointTransforms.ForEach(waypointTransform => _waypoints.Add(waypointTransform.position));
        }

        protected override void Initialize()
        {
            _controller.Initialize(_objectToMove, new List<ITrigger>(_moveNextTriggers),
                new List<ITrigger>(_movePreviousTriggers), _waypoints, _movementSpeed, _settings,
                _platformColliderTrigger);
        }
    }
}