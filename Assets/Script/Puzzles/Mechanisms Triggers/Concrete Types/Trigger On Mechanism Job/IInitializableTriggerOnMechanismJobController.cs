using System.Collections.Generic;
using Puzzles.Mechanisms;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Mechanism_Job
{
    public interface IInitializableTriggerOnMechanismJobController
    {
        public void Initialize(MechanismJobEventType eventType, List<IMechanismController> mechanismController,
            MechanismsTriggerBaseSetupData setupData);
    }
}