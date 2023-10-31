using DG.Tweening;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.View
{
    public abstract class SpellSlotGroupViewBase : ISpellSlotGroupViewBase
    {
        private readonly Vector3 _defaultLocalScale;
        private readonly GameObject _gameObject;
        private readonly RectTransform _rectTransform;
        private readonly ISpellGroupSection _settings;

        protected SpellSlotGroupViewBase(RectTransform rectTransform, ISpellGroupSection settings)
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

        public void PlayEmptyAnimation()
        {
            _rectTransform.DOComplete();
            _rectTransform.DOPunchScale(_settings.EmptyAnimationPunchStrength, _settings.EmptyAnimationDuration,
                              _settings.EmptyAnimationPunchVibratoCount, _settings.EmptyAnimationPunchElasticity)
                          .ApplyCustomSetupForUI(_gameObject);
        }

        private void ChangeScaleWithAnimation(float newScaleCoefficient)
        {
            _rectTransform.DOKill();
            Vector3 newLocalScaleValue = _defaultLocalScale * newScaleCoefficient;
            _rectTransform.DOScale(newLocalScaleValue, _settings.SelectionAnimationDuration)
                          .SetEase(_settings.SelectionAnimationEase)
                          .ApplyCustomSetupForUI(_gameObject);
        }
    }
}