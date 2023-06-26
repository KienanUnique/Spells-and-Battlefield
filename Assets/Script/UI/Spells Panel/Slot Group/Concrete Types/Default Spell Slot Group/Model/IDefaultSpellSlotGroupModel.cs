﻿using System;
using UI.Spells_Panel.Slot_Group.Base.Model;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model
{
    public interface IDefaultSpellSlotGroupModel : ISpellSlotGroupModelBase
    {
        public event Action<int> SpellsCountChanged;
    }
}