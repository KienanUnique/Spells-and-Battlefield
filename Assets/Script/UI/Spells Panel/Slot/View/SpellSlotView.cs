using Settings.UI;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Spells_Panel.Slot.View
{
    public class SpellSlotView : ISpellSlotView
    {
        private readonly RawImage _image;
        private readonly RectTransform _rectTransform;
        private readonly SpellPanelSettings _settings;

        public SpellSlotView(RawImage image, RectTransform rectTransform, SpellPanelSettings settings)
        {
            _image = image;
            _rectTransform = rectTransform;
            _settings = settings;
        }

        public void Appear(ISlotInformation slot, Sprite sprite)
        {
            _rectTransform.localPosition = slot.LocalPosition;
            _rectTransform.localScale = slot.LocalScale;
            _image.texture = sprite.texture;
            _rectTransform.gameObject.SetActive(true);
        }

        public void MoveToSlot(ISlotInformation slot)
        {
            _rectTransform.localPosition = slot.LocalPosition;
            _rectTransform.localScale = slot.LocalScale;
        }

        public void Disappear()
        {
            _rectTransform.gameObject.SetActive(false);
        }
    }
}