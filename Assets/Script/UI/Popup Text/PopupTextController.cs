using System;
using System.Collections;
using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using DG.Tweening;
using Settings.UI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Popup_Text
{
    public class PopupTextController : InitializableMonoBehaviourBase, IInitializablePopupTextController,
        IPopupTextController, IObjectPoolItem
    {
        private TMP_Text _textComponent;
        private Transform _mainTransform;
        private PopupTextSettings _settings;
        private IReadonlyTransform _cameraTransform;
        private Coroutine _lookAtCameraCoroutine;


        public void Initialize(TMP_Text textComponent, Transform mainTransform, PopupTextSettings settings,
            IReadonlyTransform cameraTransform)
        {
            _textComponent = textComponent;
            _mainTransform = mainTransform;
            _settings = settings;
            _cameraTransform = cameraTransform;
            SetInitializedStatus();
            gameObject.SetActive(false);
        }

        public event Action<IObjectPoolItem> NeedRelease;
        public bool IsUsed { get; private set; }
        private float HalfAnimationDurationInSeconds => _settings.AnimationDurationInSeconds / 2;

        public void Popup(string textToShow, Vector3 startPosition)
        {
            if (IsUsed)
            {
                throw new InvalidOperationException();
            }

            IsUsed = true;

            _textComponent.text = textToShow;
            _mainTransform.position = startPosition;

            gameObject.SetActive(true);

            _lookAtCameraCoroutine = StartCoroutine(LookAtCameraCoroutine());

            var needSequence = DOTween.Sequence();
            needSequence.Join(_mainTransform
                .DOMove(CalculatePositionToMove(_cameraTransform.Position), _settings.AnimationDurationInSeconds)
                .SetEase(_settings.MovementEase).SetLink(_mainTransform.gameObject));
            needSequence.Append(_mainTransform.DOScale(Vector3.one, HalfAnimationDurationInSeconds)
                .SetEase(_settings.ScaleEase).SetLink(_mainTransform.gameObject));
            needSequence.Append(_mainTransform.DOScale(Vector3.zero, HalfAnimationDurationInSeconds)
                .SetEase(_settings.ScaleEase).SetLink(_mainTransform.gameObject));
            needSequence.OnComplete(OnPopupAnimationComplete);
        }

        private IEnumerator LookAtCameraCoroutine()
        {
            _mainTransform.LookAt(_cameraTransform.Position);
            yield return null;
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        private void OnPopupAnimationComplete()
        {
            _mainTransform.DOKill();
            _mainTransform.localScale = Vector3.zero;
            StopCoroutine(_lookAtCameraCoroutine);
            IsUsed = false;
            gameObject.SetActive(false);
            NeedRelease?.Invoke(this);
        }

        private Vector3 CalculatePositionToMove(Vector3 cameraPosition)
        {
            var circleCenter = _mainTransform.position;
            var circleNormalVector = (circleCenter - cameraPosition).normalized;

            var randomAngle = Random.Range(0f, 2f * Mathf.PI);
            var randomRadius = Random.Range(_settings.MoveMinimumRadius, _settings.MoveMaximumRadius);

            return circleCenter + randomRadius *
                (Mathf.Cos(randomAngle) * circleNormalVector + Mathf.Sin(randomAngle) * Vector3.up);
        }
    }
}