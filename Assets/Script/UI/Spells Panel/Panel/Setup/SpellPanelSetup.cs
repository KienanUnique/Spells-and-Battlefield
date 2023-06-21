using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Player.Spell_Manager;
using UI.Spells_Panel.Panel.Model;
using UI.Spells_Panel.Panel.Presenter;
using UI.Spells_Panel.Slot_Group;
using UI.Spells_Panel.Slot_Group.Presenter;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Spells_Panel.Panel.Setup
{
    public class SpellPanelSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<SpellSlotGroupPresenter> _spellGroups;
        private IPlayerSpellsManagerInformation _managerInformation;
        private IInitializableSpellPanelPresenter _presenter;


        [Inject]
        private void Construct(IPlayerSpellsManagerInformation managerInformation)
        {
            _managerInformation = managerInformation;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization
        {
            get
            {
                var initializableObjects = new List<IInitializable>();
                foreach (var slot in _spellGroups)
                {
                    if (slot is IInitializable initializableSlot)
                    {
                        initializableObjects.Add(initializableSlot);
                    }
                }

                return initializableObjects;
            }
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableSpellPanelPresenter>();
        }

        protected override void Initialize()
        {
            var model = new SpellPanelModel(new List<ISpellSlotGroup>(_spellGroups), _managerInformation);
            _presenter.Initialize(new List<IDisableable> {model});
        }
    }
}