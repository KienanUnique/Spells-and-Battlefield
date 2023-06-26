using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Spells_Panel.Slot_Group.Base.Presenter;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Presenter
{
    
    public class LastChanceSpellSlotGroupPresenter : SpellSlotGroupPresenterBase,
        IIInitializableLastChanceSpellSlotGroupPresenter
    {
        public void Initialize(ILastChanceSpellGroupModel model, ILastChanceSpellSlotGroupView view,
            List<IDisableable> itemsNeedDisabling)
        {
            base.Initialize(model, view, itemsNeedDisabling);
        }
    }
}