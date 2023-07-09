using System.Collections.Generic;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Triggers;
using UnityEngine;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform_With_Stops
{
    public class MovingPlatformWithStopsControllerSetup : MovingPlatformControllerSetupBase
    {
        [SerializeField] private List<TriggerBase> _moveNextTriggers;
        [SerializeField] private List<TriggerBase> _movePreviousTriggers;
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
            _controller.Initialize(new List<ITrigger>(_moveNextTriggers), new List<ITrigger>(_movePreviousTriggers),
                dataForControllerBase);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _controller = GetComponent<IInitializableMovingPlatformWithStopsController>();
        }
    }
}