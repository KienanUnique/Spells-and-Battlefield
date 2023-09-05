using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using Player;
using Player.Spell_Manager;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Spells_Panel.Settings;
using UI.Spells_Panel.Settings.Sections;
using UI.Spells_Panel.Settings.Sections.Group;
using UI.Spells_Panel.Slot.Presenter;
using UI.Spells_Panel.Slot_Group.Base.Model;
using UI.Spells_Panel.Slot_Group.Base.View;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Spells_Panel.Slot_Group.Base.Setup
{
    public abstract class SpellSlotGroupSetupBase<TModel, TView, TPresenter> : SetupMonoBehaviourBase
        where TModel : ISpellSlotGroupModelBaseWithDisabling
        where TView : ISpellSlotGroupViewBase
        where TPresenter : IInitializableSpellSlotGroupPresenter<TModel, TView>
    {
        [SerializeField] private List<SpellSlotPresenter> _slots;
        [SerializeField] private RectTransform _rectTransform;
        private TPresenter _presenter;
        private List<IDisableable> _itemsNeedDisabling;
        private IPlayerSpellsManagerInformation _managerInformation;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private ISpellGroupSection _settings;

        [Inject]
        public void Construct(ISpellPanelSettings settings, IPlayerSpellsManagerInformation managerInformation,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _settings = settings.GroupSection;
            _managerInformation = managerInformation;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected abstract ISpellType TypeToRepresent { get; }

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

                initializableObjects.Add(_playerInitializationStatus);

                return initializableObjects;
            }
        }

        protected abstract TModel CreateModel(
            IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<SpellSlotPresenter> slots, IReadonlyListWithReactionOnChange<ISpell> spellsGroupToRepresent);

        protected abstract TView CreateView(RectTransform rectTransform,
            ISpellGroupSection settings, int count);

        protected override void Prepare()
        {
            _presenter = GetComponent<TPresenter>();
            _itemsNeedDisabling = new List<IDisableable>();
        }

        protected override void Initialize()
        {
            var slotsInformation = new SortedSet<ISlotInformation>();
            foreach (var slot in _slots)
            {
                slotsInformation.Add(slot.CurrentSlotInformation);
            }

            var spellsGroupToRepresent = _managerInformation.Spells[TypeToRepresent];

            var model = CreateModel(slotsInformation, _slots, spellsGroupToRepresent);
            var view = CreateView(_rectTransform, _settings, spellsGroupToRepresent.Count);

            _itemsNeedDisabling.Add(model);
            _presenter.Initialize(model, view, _itemsNeedDisabling);
        }
    }
}