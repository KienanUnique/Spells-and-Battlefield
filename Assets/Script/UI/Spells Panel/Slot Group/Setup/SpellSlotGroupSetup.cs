using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Player.Spell_Manager;
using Settings.UI;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using TMPro;
using UI.Spells_Panel.Slot.Presenter;
using UI.Spells_Panel.Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.View;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Spells_Panel.Slot_Group.Setup
{
    public class SpellSlotGroupSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private SpellTypeScriptableObject _typeToRepresent;
        [SerializeField] private List<SpellSlotPresenter> _slots;
        [SerializeField] private TMP_Text _spellsCountText;
        private IInitializableSpellSlotGroupPresenter _presenter;
        private List<IDisableable> _itemsNeedDisabling;
        private SpellPanelSettings _settings;
        private IPlayerSpellsManagerInformation _managerInformation;

        [Inject]
        public void Construct(SpellPanelSettings settings, IPlayerSpellsManagerInformation managerInformation)
        {
            _settings = settings;
            _managerInformation = managerInformation;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization
        {
            get
            {
                var initializableObjects = new List<IInitializable>();
                foreach (var slot in _slots)
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
            _presenter = GetComponent<IInitializableSpellSlotGroupPresenter>();
            _itemsNeedDisabling = new List<IDisableable>();
        }

        protected override void Initialize()
        {
            var slotsInformation = new SortedSet<ISlotInformation>();
            foreach (var slot in _slots)
            {
                slotsInformation.Add(slot.CurrentSlotInformation);
            }

            var typeToRepresent = _typeToRepresent.GetImplementationObject();
            var spellsGroupToRepresent = _managerInformation.Spells[typeToRepresent];

            var model = new SpellSlotGroupModel(slotsInformation, _slots, spellsGroupToRepresent, typeToRepresent);

            var view = new SpellSlotGroupView(_spellsCountText, transform, _settings);

            _itemsNeedDisabling.Add(model);
            _presenter.Initialize(model, view, _itemsNeedDisabling);
        }
    }
}