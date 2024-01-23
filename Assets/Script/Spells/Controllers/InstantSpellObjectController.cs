using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Activation;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Factory;
using UnityEngine;

namespace Spells.Controllers
{
    public class InstantSpellObjectController :
        SpellObjectControllerBase<IDataForActivationInstantSpellObjectController>,
        IInitializableInstantSpellController
    {
        private IDataForInstantSpellControllerFromSetupScriptableObjects _spellControllerFromSetupScriptableObjectsData;

        public void Initialize(IDataForInstantSpellControllerFromSetupScriptableObjects spellControllerData,
            ICaster caster, ISpellObjectsFactory spellObjectsFactory, IReadonlyTransform castPoint)
        {
            _spellControllerFromSetupScriptableObjectsData = spellControllerData;
            SetInitializedStatus();
        }

        protected override void CheckTime(float timePassedFromActivation)
        {
            base.CheckTime(timePassedFromActivation);
            HandleSpellTriggerResponse(
                _spellControllerFromSetupScriptableObjectsData.SpellMainTrigger.CheckTime(timePassedFromActivation));
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            base.HandleTriggerEnter(other);
            HandleSpellTriggerResponse(
                _spellControllerFromSetupScriptableObjectsData.SpellMainTrigger.CheckContact(other));
        }

        protected override void HandleFinishSpell()
        {
            Transform spellTransform = SpellRigidbody.transform;
            var positionForNextSpell =
                new PositionDataForInstantiation(spellTransform.position, spellTransform.rotation);
            foreach (IInformationAboutInstantSpell spell in _spellControllerFromSetupScriptableObjectsData
                         .NextSpellsOnFinish)
            {
                SpellObjectsFactory.Create(spell.GetActivationData(Caster), spell.PrefabProvider, positionForNextSpell);
            }

            base.HandleFinishSpell();
        }

        private void HandleSpellTriggerResponse(SpellTriggerCheckStatusEnum response)
        {
            if (response == SpellTriggerCheckStatusEnum.Finish)
            {
                HandleFinishSpell();
            }
        }
    }
}