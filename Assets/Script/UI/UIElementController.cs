using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIElementController : MonoBehaviour, IElementUI
    {
        [SerializeField] private float _scaleAnimationDuration;
        private Transform _cashedTransform;
        private GameObject _cashedGameObject;

        private void Awake()
        {
            _cashedTransform = transform;
            _cashedGameObject = gameObject;

            _cashedTransform.localScale = Vector3.zero;
            _cashedGameObject.SetActive(false);
        }

        public virtual void Appear()
        {
            _cashedGameObject.SetActive(true);
            _cashedTransform.DOKill();
            _cashedTransform.DOScale(Vector3.one, _scaleAnimationDuration);
        }

        public virtual void Disappear()
        {
            _cashedTransform.DOKill();
             _cashedTransform.DOScale(Vector3.zero, _scaleAnimationDuration).OnComplete(() =>
                 _cashedGameObject.SetActive(false));
        }
    }
}