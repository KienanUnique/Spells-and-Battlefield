﻿using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Setup;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.View;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Presenter
{
    [RequireComponent(typeof(SpellSlotSetup))]
    public class SpellSlotPresenter : InitializableMonoBehaviourBase, ISpellSlot, IInitializableSpellSlotPresenter
    {
        private ISpellSlotModel _model;
        private ISpellSlotView _view;

        public void Initialize(ISpellSlotModel model, ISpellSlotView view, ISpellPanelSettings settings)
        {
            _model = model;
            _view = view;

            SetInitializedStatus();
        }

        public bool IsEmptySlot => _model.IsEmptySlot;
        public ISlotInformation CurrentSlotInformation => _model.CurrentSlotInformation;
        public ISpell CurrentSpell => _model.CurrentSpell;

        public void MoveToSlot(ISlotInformation slot)
        {
            _model.MoveToSlot(slot);
            _view.MoveToSlot(slot);
        }

        public void AppearAsSlot(ISlotInformation slot, ISpell spellToRepresent)
        {
            _model.Appear(spellToRepresent, slot, false);
            _view.Appear(slot, spellToRepresent.CardInformation.Icon);
        }

        public void AppearAsEmptySlot(ISlotInformation slot)
        {
            _model.Appear(null, slot, true);
            _view.AppearAsEmptySlot(slot);
        }

        public void DisappearAndForgetSpell()
        {
            _model.Disappear();
            _view.Disappear();
        }

        public void ChangeBackgroundColor(Color newBackgroundColor)
        {
            _view.ChangeBackgroundColor(newBackgroundColor);
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
                    if (_model.IsVisible)
                    {
                        if (_model.CurrentSpell == null)
                        {
                            _view.AppearAsEmptySlot(_model.CurrentSlotInformation);
                        }
                        else
                        {
                            _view.Appear(_model.CurrentSlotInformation, _model.CurrentSpell.CardInformation.Icon);
                        }
                    }
                    else
                    {
                        _view.Disappear();
                    }

                    break;
                case InitializableMonoBehaviourStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }
    }
}