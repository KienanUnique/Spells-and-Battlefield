﻿using DG.Tweening;
using Spells.Implementations_Interfaces.Implementations;
using UI.Spells_Panel.Settings;
using UI.Spells_Panel.Settings.Sections;
using UI.Spells_Panel.Settings.Sections.Group;
using UnityEngine;

namespace UI.Spells_Panel.Slot_Group.Base.View
{
    public abstract class SpellSlotGroupViewBase : ISpellSlotGroupViewBase
    {
        private readonly RectTransform _rectTransform;
        private readonly ISpellGroupSection _settings;
        private readonly Vector3 _defaultLocalScale;
        private readonly GameObject _gameObject;

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
                .SetLink(_gameObject);
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