using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.View;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View
{
    public class LastChanceSpellSlotGroupView : SpellSlotGroupViewBase, ILastChanceSpellSlotGroupView
    {
        public LastChanceSpellSlotGroupView(RectTransform rectTransform, ISpellGroupSection settings) : base(
            rectTransform, settings)
        {
        }
    }
}