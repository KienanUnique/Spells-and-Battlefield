using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Spells.Implementations_Interfaces.Implementations;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.View;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Presenter
{
    public abstract class SpellSlotGroupPresenterBase : InitializableMonoBehaviourBase, ISpellSlotGroup
    {
        private ISpellSlotGroupModelBase _model;
        private ISpellSlotGroupViewBase _view;

        protected void Initialize(ISpellSlotGroupModelBase model, ISpellSlotGroupViewBase view,
            List<IDisableable> itemsNeedDisabling)
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

        public void PlayAnimationOnTryingToUseEmptySpellTypeGroup()
        {
            _view.PlayEmptyAnimation();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
        }

        private void OnInitializationStatusChanged(InitializableMonoBehaviourStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializableMonoBehaviourStatus.Initialized:
                    if (_model.IsSelected)
                    {
                        _view.Unselect();
                    }

                    if (_model.IsSelected)
                    {
                        _view.Select();
                    }

                    break;
                case InitializableMonoBehaviourStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }
    }
}