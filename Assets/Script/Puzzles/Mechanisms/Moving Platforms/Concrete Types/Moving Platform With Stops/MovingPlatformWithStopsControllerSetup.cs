using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform_With_Stops
{
    public class MovingPlatformWithStopsControllerSetup : MovingPlatformControllerSetupBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _moveNextTriggers;
        [SerializeField] private List<MechanismsTriggerBase> _movePreviousTriggers;
        private IInitializableMovingPlatformWithStopsController _controller;

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

        protected override void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _controller.Initialize(new List<IMechanismsTrigger>(_moveNextTriggers),
                new List<IMechanismsTrigger>(_movePreviousTriggers), dataForControllerBase);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _controller = GetComponent<IInitializableMovingPlatformWithStopsController>();
        }
    }
}