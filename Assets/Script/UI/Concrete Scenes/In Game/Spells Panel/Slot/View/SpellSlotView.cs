﻿using DG.Tweening;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Slot;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Information;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.View
{
    public class SpellSlotView : ISpellSlotView
    {
        private readonly Image _background;
        private readonly GameObject _gameObject;
        private readonly RawImage _image;
        private readonly RectTransform _rectTransform;
        private readonly ISpellSlotSection _settings;

        public SpellSlotView(RawImage image, RectTransform rectTransform, Image background, ISpellSlotSection settings)
        {
            _background = background;
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
                          .SetEase(_settings.ScaleAnimationEase)
                          .ApplyCustomSetupForUI(_gameObject);
        }

        public void AppearAsEmptySlot(ISlotInformation slot)
        {
            Appear(slot, _settings.EmptySlotIcon);
        }

        public void MoveToSlot(ISlotInformation slot)
        {
            _rectTransform.DOKill();
            _rectTransform.DOLocalMove(slot.LocalPosition, _settings.MoveAnimationDuration)
                          .SetEase(_settings.MoveAnimationEase)
                          .ApplyCustomSetupForUI(_gameObject);
            _rectTransform.DOScale(slot.LocalScale, _settings.MoveAnimationDuration)
                          .SetEase(_settings.ScaleDuringMovingAnimationEase)
                          .ApplyCustomSetupForUI(_gameObject);
        }

        public void Disappear()
        {
            _rectTransform.DOKill();
            _rectTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                          .SetEase(_settings.ScaleDuringMovingAnimationEase)
                          .ApplyCustomSetupForUI(_gameObject)
                          .OnComplete(() => _gameObject.SetActive(false));
        }

        public void ChangeBackgroundColor(Color newBackgroundColor)
        {
            _background.color = newBackgroundColor;
        }
    }
}