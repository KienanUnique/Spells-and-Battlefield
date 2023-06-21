using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Spells.Implementations_Interfaces.Implementations;
using UI.Spells_Panel.Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Setup;
using UI.Spells_Panel.Slot_Group.View;

namespace UI.Spells_Panel.Slot_Group.Presenter
{
    public class SpellSlotGroupPresenter : InitializableMonoBehaviourBase, ISpellSlotGroup, IInitializableSpellSlotGroupPresenter
    {
        private ISpellSlotGroupModel _model;
        private ISpellSlotGroupView _view;

        public void Initialize(ISpellSlotGroupModel model, ISpellSlotGroupView view, List<IDisableable> itemsNeedDisabling)
        {
            _model = model;
            _view = view;
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }

        public bool IsSelected => _model.IsSelected;

        public ISpellType Type => _model.Type;

        public void Select()
        {
            _model.Select();
            _view.Select();
        }

        public void Unselect()
        {
            _model.Unselect();
            _view.Unselect();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializationStatus.Initialized:
                    if (_model.IsSelected)
                    {
                        _view.Unselect();
                    }

                    if (_model.IsSelected)
                    {
                        _view.Select();
                    }

                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }
    }
}