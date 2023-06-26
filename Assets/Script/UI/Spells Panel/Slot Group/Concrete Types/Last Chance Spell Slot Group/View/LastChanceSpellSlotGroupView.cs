using Settings.UI;
using Settings.UI.Spell_Panel;
using UI.Spells_Panel.Slot_Group.Base.View;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View
{
    public class LastChanceSpellSlotGroupView : SpellSlotGroupViewBase, ILastChanceSpellSlotGroupView
    {
        public LastChanceSpellSlotGroupView(RectTransform rectTransform, SpellGroupSection settings)
            : base(rectTransform, settings)
        {
        }
    }
}