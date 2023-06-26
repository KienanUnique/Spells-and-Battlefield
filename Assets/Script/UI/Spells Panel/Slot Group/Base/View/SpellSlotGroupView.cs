using Settings.UI;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Base.View
{
    public abstract class SpellSlotGroupViewBase : ISpellSlotGroupViewBase
    {
        private readonly RectTransform _rectTransform;
        private readonly SpellPanelSettings _settings;

        protected SpellSlotGroupViewBase(RectTransform rectTransform, SpellPanelSettings settings)
        {
            _rectTransform = rectTransform;
            _settings = settings;
        }

        public void Select()
        {
            //_rectTransform.sizeDelta = _settings.SelectedGroupSizeDelta;
        }

        public void Unselect()
        {
            //_rectTransform.sizeDelta = _settings.UnselectedGroupSizeDelta;
        }
    }
}