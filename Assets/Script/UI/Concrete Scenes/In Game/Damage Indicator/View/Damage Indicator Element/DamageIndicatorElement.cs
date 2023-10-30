using DG.Tweening;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.View.Damage_Indicator_Element
{
    public class DamageIndicatorElement : IDamageIndicatorElement
    {
        private readonly IDamageIndicatorElementSettings _settings;
        private readonly Image _indicatorImage;

        public DamageIndicatorElement(IDamageIndicatorElementSettings settings, Image indicatorImage)
        {
            _settings = settings;
            _indicatorImage = indicatorImage;
            Color needColor = indicatorImage.color;
            needColor.a = 0f;
            indicatorImage.color = needColor;
        }

        public void AppearTemporarily()
        {
            _indicatorImage.DOKill();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_indicatorImage.DOFade(1f, _settings.AppearFadeDuration).SetEase(_settings.AppearFadeEase));
            sequence.Append(_indicatorImage.DOFade(0f, _settings.DisappearFadeDuration)
                                           .SetEase(_settings.DisappearFadeEase));
            sequence.SetLink(_indicatorImage.gameObject);
        }
    }
}