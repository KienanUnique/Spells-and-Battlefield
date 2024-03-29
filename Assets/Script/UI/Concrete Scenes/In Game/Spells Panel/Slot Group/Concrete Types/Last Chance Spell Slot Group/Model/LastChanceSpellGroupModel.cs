﻿using System.Collections.Generic;
using Common.Collection_With_Reaction_On_Change;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model
{
    public class LastChanceSpellGroupModel : SpellSlotGroupModelBase, ILastChanceSpellGroupModelWithDisabling
    {
        public LastChanceSpellGroupModel(IEnumerable<ISlotInformation> slotsInformation,
            IEnumerable<ISpellSlot> slotControllers, IReadonlyListWithReactionOnChange<ISpell> spellGroupToRepresent,
            ISpellType spellTypeToRepresent) : base(slotsInformation, slotControllers, spellGroupToRepresent,
            spellTypeToRepresent)
        {
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}