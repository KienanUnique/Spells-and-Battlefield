using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Spells_Panel.Slot_Group.Base.Presenter;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Presenter
{
    [RequireComponent(typeof(DefaultSpellSlotGroupSetup))]
    public class DefaultSpellGroupPresenter : SpellSlotGroupPresenterBase, IIInitializableDefaultSpellGroupPresenter
    {
        private IDefaultSpellSlotGroupModel _model;
        private IDefaultSpellSlotGroupView _view;

        public void Initialize(IDefaultSpellSlotGroupModel model, IDefaultSpellSlotGroupView view,
            List<IDisableable> itemsNeedDisabling)
        {
            _model = model;
            _view = view;
            base.Initialize(model, view, itemsNeedDisabling);
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _model.SpellsCountChanged += _view.UpdateGroupCount;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _model.SpellsCountChanged -= _view.UpdateGroupCount;
        }
    }
}