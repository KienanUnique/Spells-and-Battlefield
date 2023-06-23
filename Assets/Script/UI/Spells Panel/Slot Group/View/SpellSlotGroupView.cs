using Settings.UI;
using TMPro;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.View
{
    public class SpellSlotGroupView : ISpellSlotGroupView
    {
        private readonly TMP_Text _spellsCountText;
        private readonly RectTransform _rectTransform;
        private readonly SpellPanelSettings _settings;

        public SpellSlotGroupView(TMP_Text spellsCountText, RectTransform rectTransform, SpellPanelSettings settings)
        {
            _spellsCountText = spellsCountText;
            _rectTransform = rectTransform;
            _settings = settings;
        }

        public void UpdateGroupCount(int newCount)
        {
            _spellsCountText.text = newCount.ToString();
        }

        public void Select()
        {
            _rectTransform.sizeDelta = _settings.SelectedGroupSizeDelta;
        }

        public void Unselect()
        {
            _rectTransform.sizeDelta = _settings.UnselectedGroupSizeDelta;
        }
    }
}