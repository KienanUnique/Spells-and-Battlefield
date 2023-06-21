using Settings.UI;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Spells_Panel.Slot.View
{
    public class SpellSlotView : ISpellSlotView
    {
        private readonly RawImage _image;
        private readonly Transform _transform;
        private readonly SpellPanelSettings _settings;

        public SpellSlotView(RawImage image, Transform transform, SpellPanelSettings settings)
        {
            _image = image;
            _transform = transform;
            _settings = settings;
        }

        public void Appear(ISlotInformation slot, Sprite sprite)
        {
            _transform.position = slot.Position;
            _transform.localScale = slot.LocalScale;
            _image.texture = sprite.texture;
            _transform.gameObject.SetActive(true);
        }

        public void MoveToSlot(ISlotInformation slot)
        {
            _transform.position = slot.Position;
            _transform.localScale = slot.LocalScale;
        }

        public void Disappear()
        {
            _transform.gameObject.SetActive(false);
        }
    }
}