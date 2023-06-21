using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Settings.UI;
using Spells.Spell;
using UI.Spells_Panel.Slot.Model;
using UI.Spells_Panel.Slot.Setup;
using UI.Spells_Panel.Slot.View;
using UI.Spells_Panel.Slot_Controller;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Spells_Panel.Slot.Presenter
{
    [RequireComponent(typeof(SpellSlotSetup))]
    public class SpellSlotPresenter : InitializableMonoBehaviourBase, ISpellSlot, IInitializableSpellSlotPresenter
    {
        private ISpellSlotModel _model;
        private ISpellSlotView _view;
        private SpellPanelSettings _settings;

        public void Initialize(ISpellSlotModel model, ISpellSlotView view, SpellPanelSettings settings)
        {
            _settings = settings;
            _model = model;
            _view = view;
            
            SetInitializedStatus();
        }

        public ISlotInformation CurrentSlotInformation => _model.CurrentSlotInformation;
        public ISpell CurrentSpell => _model.CurrentSpell;

        public void MoveToSlot(ISlotInformation slot)
        {
            _model.MoveToSlot(slot);
            _view.MoveToSlot(slot);
        }

        public void AppearAsSlot(ISlotInformation slot, ISpell spellToRepresent)
        {
            _model.Appear(spellToRepresent, slot);
            _view.Appear(slot, spellToRepresent.CardInformation.Icon);
        }

        public void AppearAsEmptySlot(ISlotInformation slot)
        {
            _model.Appear(null, slot);
            _view.Appear(slot, _settings.EmptySlotIcon);
        }

        public void DisappearAndForgetSpell()
        {
            _model.Disappear();
            _view.Disappear();
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
                    if (_model.IsVisible)
                    {
                        _view.Appear(_model.CurrentSlotInformation,
                            _model.CurrentSpell == null
                                ? _settings.EmptySlotIcon
                                : _model.CurrentSpell.CardInformation.Icon);
                    }
                    else
                    {
                        _view.Disappear();
                    }

                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }
    }
}