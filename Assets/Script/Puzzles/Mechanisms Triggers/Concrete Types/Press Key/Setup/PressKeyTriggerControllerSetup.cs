using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Press_Key.Setup
{
    public class PressKeyTriggerControllerSetup : MechanismsTriggerSetupBase
    {
        private IInitializablePressKeyTriggerController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePressKeyTriggerController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(BaseSetupData);
        }
    }
}