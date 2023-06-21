using Settings.UI;
using TMPro;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.View
{
    public class SpellSlotGroupView : ISpellSlotGroupView
    {
        private readonly TMP_Text _spellsCountText;
        private readonly Transform _transform;
        private readonly SpellPanelSettings _settings;

        public SpellSlotGroupView(TMP_Text spellsCountText, Transform transform, SpellPanelSettings settings)
        {
            _spellsCountText = spellsCountText;
            _transform = transform;
            _settings = settings;
        }

        public void UpdateGroupCount(int newCount)
        {
            _spellsCountText.text = newCount.ToString();
        }

        public void Select()
        {
            _transform.localScale = _settings.SelectedGroupLocalScale;
        }

        public void Unselect()
        {
            _transform.localScale = _settings.UnselectedGroupLocalScale;
        }
    }
}