using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform
{
    public class MovingPlatformControllerSetup : MovingPlatformControllerSetupBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _triggers;
        private IInitializableMovingPlatformController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(_triggers);

        protected override void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _controller.Initialize(new List<IMechanismsTrigger>(_triggers), dataForControllerBase);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _controller = GetComponent<IInitializableMovingPlatformController>();
        }
    }
}