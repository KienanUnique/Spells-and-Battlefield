using DG.Tweening;
using Settings.UI.Spell_Panel;
using UI.Spells_Panel.Slot_Information;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Spells_Panel.Slot.View
{
    public class SpellSlotView : ISpellSlotView
    {
        private readonly RawImage _image;
        private readonly RectTransform _rectTransform;
        private readonly SpellSlotSection _settings;
        private readonly GameObject _gameObject;

        public SpellSlotView(RawImage image, RectTransform rectTransform, SpellSlotSection settings)
        {
            _image = image;
            _rectTransform = rectTransform;
            _settings = settings;
            _gameObject = rectTransform.gameObject;
        }

        public void Appear(ISlotInformation slot, Sprite sprite)
        {
            _rectTransform.localPosition = slot.LocalPosition;
            _rectTransform.localScale = Vector3.zero;
            _image.texture = sprite.texture;

            _gameObject.SetActive(true);
            _rectTransform.DOKill();
            _rectTransform.DOScale(slot.LocalScale, _settings.ScaleAnimationDuration)
                .SetEase(_settings.ScaleAnimationEase).SetLink(_gameObject);
        }

        public void AppearAsEmptySlot(ISlotInformation slot)
        {
            Appear(slot, _settings.EmptySlotIcon);
        }

        public void MoveToSlot(ISlotInformation slot)
        {
            _rectTransform.DOKill();
            _rectTransform.DOLocalMove(slot.LocalPosition, _settings.MoveAnimationDuration)
                .SetEase(_settings.MoveAnimationEase).SetLink(_gameObject);
            _rectTransform.DOScale(slot.LocalScale, _settings.MoveAnimationDuration)
                .SetEase(_settings.ScaleDuringMovingAnimationEase).SetLink(_gameObject);
        }

        public void Disappear()
        {
            _rectTransform.DOKill();
            _rectTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                .SetEase(_settings.ScaleDuringMovingAnimationEase).SetLink(_gameObject)
                .OnComplete(() => _gameObject.SetActive(false));
        }
    }
}