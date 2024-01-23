using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Controllers.Data_For_Controller;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Controllers.Data_For_Controller_Activation
{
    public interface IBaseDataForSpellControllerActivation
    {
        IBaseDataForSpellController BaseDataForSpellController { get; }
        ICaster Caster { get; }
        IBaseDataForSpellController ControllerData { get; }
        IReadonlyTransform CastPoint { get; }

        public void Initialize(Rigidbody spellRigidbody, ICoroutineStarter coroutineStarter,
            ISpellTargetsDetector targetsDetector);
    }
}