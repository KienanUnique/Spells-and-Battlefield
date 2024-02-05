using System.Collections.Generic;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms.Dissolve_Object.Concrete_Types.Default;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public class DissolveObjectMechanismController : DissolveObjectMechanismControllerBase,
        IInitializableDissolveObjectMechanismController
    {
        public void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> triggers,
            IDissolveEffectController dissolveEffectController, List<IColliderWithDisabling> collidersToDisable)
        {
            AddTriggers(triggers);
            InitializeBase(isEnabledAtStart, dissolveEffectController, collidersToDisable);
        }

        protected override void StartJob()
        {
            ChangeState(!IsEnabled);
        }
    }
}