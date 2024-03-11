using DG.Tweening;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.View
{
    public class ContinuousEffectIndicatorView : IContinuousEffectIndicatorView
    {
        private const float FullFillAmount = 1f;

        private readonly Slider _slider;
        private readonly RawImage _image;
        private readonly Transform _cachedTransform;
        private readonly GameObject _cachedGameObject;
        private readonly Transform _originalParent;
        private readonly IContinuousEffectIndicatorSettings _settings;

        public ContinuousEffectIndicatorView(Slider slider, RawImage image, Transform cachedTransform,
            IContinuousEffectIndicatorSettings settings)
        {
            _slider = slider;
            _image = image;
            _cachedTransform = cachedTransform;
            _cachedGameObject = _cachedTransform.gameObject;
            _originalParent = _cachedTransform.parent;
            _settings = settings;
        }

        public void Appear(Sprite icon, Transform parent)
        {
            _slider.value = FullFillAmount;
            _image.texture = icon.texture;
            _cachedTransform.SetParent(parent);
            _cachedGameObject.SetActive(true);
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.one, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.AppearScaleAnimationEase);
        }

        public void UpdateRatioOfCompletedPartToEntireDuration(float newRatio)
        {
            _slider.value = newRatio;
        }

        public void Disappear()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                            .ApplyCustomSetupForUI(_cachedGameObject)
                            .SetEase(_settings.DisappearScaleAnimationEase)
                            .OnComplete(DisappearWithoutAnimation);
        }

        public void DisappearWithoutAnimation()
        {
            _cachedGameObject.SetActive(false);
            _cachedTransform.SetParent(_originalParent);
        }
    }
}