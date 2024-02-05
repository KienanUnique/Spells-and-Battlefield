using System.Collections.Generic;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms.Dissolve_Object.Concrete_Types.Split
{
    public interface IInitializableSplitDissolveObjectMechanismController
    {
        void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> disappearTriggers,
            List<IMechanismsTrigger> appearTriggers, IDissolveEffectController dissolveEffectController,
            List<IColliderWithDisabling> collidersToDisable);
    }
}