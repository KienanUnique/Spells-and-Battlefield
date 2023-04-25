using DG.Tweening;
using Game_Managers;
using General_Settings_in_Scriptable_Objects;
using UnityEngine;

namespace UI
{
    public class UIElementController : MonoBehaviour, IElementUI
    {
        private UIAnimationSettings _settings;
        private Transform _cashedTransform;
        private GameObject _cashedGameObject;

        public virtual void Appear()
        {
            _cashedGameObject.SetActive(true);
            _cashedTransform.DOKill();
            _cashedTransform.DOScale(Vector3.one, _settings.ScaleAnimationDuration)
                .SetEase(_settings.ScaleAnimationEase).SetLink(gameObject);
        }

        public virtual void Disappear()
        {
            _cashedTransform.DOKill();
            _cashedTransform.DOScale(Vector3.zero, _settings.ScaleAnimationDuration)
                .SetEase(_settings.ScaleAnimationEase).SetLink(gameObject)
                .OnComplete(() => _cashedGameObject.SetActive(false));
        }

        protected virtual void Awake()
        {
            _cashedTransform = transform;
            _cashedGameObject = gameObject;
            _settings = SettingsProvider.Instance.UIAnimationSettings;

            _cashedTransform.localScale = Vector3.zero;
            _cashedGameObject.SetActive(false);
        }
    }
}