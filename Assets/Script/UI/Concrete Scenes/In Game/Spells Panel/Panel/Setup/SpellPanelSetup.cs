using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Player;
using Player.Spell_Manager;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Presenter;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Presenter;
using UI.Element.Setup;
using UI.Element.View;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Panel.Setup
{
    public class SpellPanelSetup : UIElementPresenterSetup
    {
        [SerializeField] private List<SpellSlotGroupPresenterBase> _spellGroups;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IPlayerSpellsManagerInformation _playerManagerInformation;
        private IInitializableSpellPanelPresenter _presenter;
        private IUIElementView _view;

        [Inject]
        private void GetDependencies(IPlayerSpellsManagerInformation managerInformation,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerManagerInformation = managerInformation;
            _playerInitializationStatus = playerInitializationStatus;
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

                initializableObjects.Add(_playerInitializationStatus);

                return initializableObjects;
            }
        }

        protected override void Initialize()
        {
            var model = new SpellPanelModel(new List<ISpellSlotGroup>(_spellGroups), _playerManagerInformation);
            _presenter.Initialize(_view, new List<IDisableable> {model});
        }

        protected override void Prepare()
        {
            _view = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
            _presenter = GetComponent<IInitializableSpellPanelPresenter>();
        }
    }
}