using TMPro;
using UI.Spells_Panel.Settings;
using UI.Spells_Panel.Settings.Sections;
using UI.Spells_Panel.Settings.Sections.Group;
using UI.Spells_Panel.Slot_Group.Base.View;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View
{
    public class DefaultSpellSlotGroupView : SpellSlotGroupViewBase, IDefaultSpellSlotGroupView
    {
        private readonly TMP_Text _spellsCountText;

        public DefaultSpellSlotGroupView(TMP_Text spellsCountText, RectTransform rectTransform,
            ISpellGroupSection settings, int currentSpellGroupCount) : base(rectTransform, settings)
        {
            _spellsCountText = spellsCountText;
            UpdateGroupCount(currentSpellGroupCount);
        }

        public void UpdateGroupCount(int newCount)
        {
            _spellsCountText.text = newCount.ToString();
        }
    }
}