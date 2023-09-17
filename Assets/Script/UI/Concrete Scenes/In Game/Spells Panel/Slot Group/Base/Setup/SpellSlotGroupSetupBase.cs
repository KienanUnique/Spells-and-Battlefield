using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Common.Collection_With_Reaction_On_Change;
using Player;
using Player.Spell_Manager;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.View;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Setup
{
    public abstract class SpellSlotGroupSetupBase<TModel, TView, TPresenter> : SetupMonoBehaviourBase
        where TModel : ISpellSlotGroupModelBaseWithDisabling
        where TView : ISpellSlotGroupViewBase
        where TPresenter : IInitializableSpellSlotGroupPresenter<TModel, TView>
    {
        [SerializeField] private List<SpellSlotPresenter> _slots;
        [SerializeField] private RectTransform _rectTransform;
        private List<IDisableable> _itemsNeedDisabling;
        private IPlayerSpellsManagerInformation _managerInformation;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private TPresenter _presenter;
        private ISpellGroupSection _settings;

        [Inject]
        public void GetDependencies(ISpellPanelSettings settings, IPlayerSpellsManagerInformation managerInformation,
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
                foreach (SpellSlotPresenter slot in _slots)
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

        protected abstract TModel CreateModel(IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<SpellSlotPresenter> slots, IReadonlyListWithReactionOnChange<ISpell> spellsGroupToRepresent);

        protected abstract TView CreateView(RectTransform rectTransform, ISpellGroupSection settings, int count);

        protected override void Initialize()
        {
            var slotsInformation = new SortedSet<ISlotInformation>();
            foreach (SpellSlotPresenter slot in _slots)
            {
                slotsInformation.Add(slot.CurrentSlotInformation);
            }

            IReadonlyListWithReactionOnChange<ISpell> spellsGroupToRepresent =
                _managerInformation.Spells[TypeToRepresent];

            TModel model = CreateModel(slotsInformation, _slots, spellsGroupToRepresent);
            TView view = CreateView(_rectTransform, _settings, spellsGroupToRepresent.Count);

            _itemsNeedDisabling.Add(model);
            _presenter.Initialize(model, view, _itemsNeedDisabling);
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<TPresenter>();
            _itemsNeedDisabling = new List<IDisableable>();
        }
    }
}