using DG.Tweening;
using Settings.UI;
using UnityEngine;

namespace UI.Element.View
{
    public class DefaultUIElementView : IUIElementView
    {
        private readonly Transform _cachedTransform;
        private readonly GameObject _cachedGameObject;
        private readonly GeneralUIAnimationSettings _settings;

        public DefaultUIElementView(Transform cachedTransform, GeneralUIAnimationSettings settings)
        {
            _cachedTransform = cachedTransform;
            _cachedGameObject = _cachedTransform.gameObject;
            _settings = settings;
            Disappear();
        }

        public virtual void Appear()
        {
            _cachedGameObject.SetActive(true);
            _cachedTransform.DOKill();
            _cachedTransform.SetupAppearAnimationForUI(_settings, _cachedGameObject);
        }

        public virtual void Disappear()
        {
            _cachedTransform.DOKill();
            _cachedTransform.SetupDisappearAnimationForUI(_settings, _cachedGameObject)
                .OnComplete(() => _cachedGameObject.SetActive(false));
        }
    }
}