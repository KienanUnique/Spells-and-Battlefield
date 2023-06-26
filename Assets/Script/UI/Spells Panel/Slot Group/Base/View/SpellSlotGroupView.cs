using DG.Tweening;
using Settings.UI.Spell_Panel;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Base.View
{
    public abstract class SpellSlotGroupViewBase : ISpellSlotGroupViewBase
    {
        private readonly RectTransform _rectTransform;
        private readonly SpellGroupSection _settings;
        private readonly Vector3 _defaultLocalScale;
        private readonly GameObject _gameObject;

        protected SpellSlotGroupViewBase(RectTransform rectTransform, SpellGroupSection settings)
        {
            _rectTransform = rectTransform;
            _settings = settings;
            _defaultLocalScale = _rectTransform.localScale;
            _gameObject = _rectTransform.gameObject;
        }

        public void Select()
        {
            ChangeScaleWithAnimation(_settings.SelectedGroupScaleCoefficient);
        }

        public void Unselect()
        {
            ChangeScaleWithAnimation(_settings.UnselectedGroupScaleCoefficient);
        }

        private void ChangeScaleWithAnimation(float newScaleCoefficient)
        {
            _rectTransform.DOKill();
            var newLocalScaleValue = _defaultLocalScale * newScaleCoefficient;
            _rectTransform.DOScale(newLocalScaleValue, _settings.SelectionAnimationDuration)
                .SetEase(_settings.SelectionAnimationEase).SetLink(_gameObject);
        }
    }
}