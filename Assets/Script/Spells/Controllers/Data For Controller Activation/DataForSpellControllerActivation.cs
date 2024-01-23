using Common.Interfaces;
using Common.Readonly_Transform;
using Spells.Controllers.Data_For_Controller;
using Spells.Data_For_Spell_Implementation;
using Spells.Spell_Interactable_Trigger;
using UnityEngine;

namespace Spells.Controllers.Data_For_Controller_Activation
{
    public abstract class
        DataForSpellControllerActivation<TConcreteSpellControllerData> : IBaseDataForSpellControllerActivation
        where TConcreteSpellControllerData : IInitializableDataForSpellControllerFromSetupScriptableObjects
    {
        protected DataForSpellControllerActivation(TConcreteSpellControllerData concreteSpellControllerData,
            ICaster caster, IBaseDataForSpellController controllerData, IReadonlyTransform castPoint)
        {
            ConcreteSpellControllerData = concreteSpellControllerData;
            Caster = caster;
            ControllerData = controllerData;
            CastPoint = castPoint;
        }

        public void Initialize(Rigidbody spellRigidbody, ICoroutineStarter coroutineStarter,
            ISpellTargetsDetector targetsDetector)
        {
            ConcreteSpellControllerData.Initialize(new DataForSpellImplementation(spellRigidbody, Caster,
                coroutineStarter, CastPoint, targetsDetector));
        }

        public TConcreteSpellControllerData ConcreteSpellControllerData { get; }
        public IBaseDataForSpellController BaseDataForSpellController => ConcreteSpellControllerData;
        public ICaster Caster { get; }
        public IBaseDataForSpellController ControllerData { get; }
        public IReadonlyTransform CastPoint { get; }
    }
}