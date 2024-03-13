using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using Spells.Controllers.Concrete_Types.Instant;
using Spells.Controllers.Concrete_Types.Instant.Data_For_Controller;
using Spells.Factory;
using UnityEngine;

namespace Spells.Controllers
{
    public class InstantSpellObjectController : SpellObjectControllerBase, IInitializableInstantSpellController
    {
        private IDataForInstantSpellController _spellControllerData;

        public void Initialize(IDataForInstantSpellController spellControllerData, ICaster caster,
            ISpellObjectsFactory spellObjectsFactory, IReadonlyTransform castPoint)
        {
            _spellControllerData = spellControllerData;
            InitializeBase(caster, spellObjectsFactory, spellControllerData, castPoint);
            SetInitializedStatus();
        }

        protected override void FixedUpdate()
        {
            if (CurrentInitializableMonoBehaviourStatus != InitializableMonoBehaviourStatus.Initialized)
            {
                return;
            }

            base.FixedUpdate();
            HandleSpellTriggerResponse(_spellControllerData.SpellMainTrigger.CheckTime(TimePassedFromInitialize));
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            base.HandleTriggerEnter(other);
            HandleSpellTriggerResponse(_spellControllerData.SpellMainTrigger.CheckContact(other));
        }

        protected override void HandleFinishSpell()
        {
            foreach (IInformationAboutInstantSpell spell in _spellControllerData.NextSpellsOnFinish)
            {
                var spellTransform = transform;
                SpellObjectsFactory.Create(spell.DataForController, spell.PrefabProvider, Caster,
                    spellTransform.position, spellTransform.rotation, CastPoint);
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