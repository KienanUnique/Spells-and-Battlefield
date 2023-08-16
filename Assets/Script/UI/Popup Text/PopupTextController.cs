using System;
using System.Collections;
using Common.Abstract_Bases.Factories.Object_Pool;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using DG.Tweening;
using Settings.UI;
using TMPro;
using UI.Popup_Text.Data_For_Activation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Popup_Text
{
    public class PopupTextController : InitializableMonoBehaviourBase, IInitializablePopupTextController,
        IPopupTextController
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
            gameObject.SetActive(false);
            SetInitializedStatus();
        }

        public event Action<IObjectPoolItem<IPopupTextControllerDataForActivation>> Deactivated;
        public bool IsUsed { get; private set; }

        public void Activate(IPopupTextControllerDataForActivation dataForActivation)
        {
            if (IsUsed)
            {
                throw new InvalidOperationException();
            }

            IsUsed = true;

            _textComponent.text = dataForActivation.TextToShow;

            _mainTransform.localScale = Vector3.zero;
            _mainTransform.SetPositionAndRotation(dataForActivation.SpawnPosition, dataForActivation.SpawnRotation);
            gameObject.SetActive(true);

            _lookAtCameraCoroutine = StartCoroutine(LookAtCameraCoroutine());

            if (_mainTransform == null)
            {
            }

            var needSequence = DOTween.Sequence();
            needSequence.SetLink(_mainTransform.gameObject);
            needSequence.Append(_mainTransform.DOScale(Vector3.one, HalfAnimationDurationInSeconds)
                .SetEase(_settings.ScaleEase));
            needSequence.Append(_mainTransform.DOScale(Vector3.zero, HalfAnimationDurationInSeconds)
                .SetEase(_settings.ScaleEase));
            needSequence.OnComplete(OnPopupAnimationComplete);

            _mainTransform
                .DOMove(CalculatePositionToMove(_cameraTransform.Position), _settings.AnimationDurationInSeconds)
                .SetEase(_settings.MovementEase).SetLink(_mainTransform.gameObject);
        }

        private float HalfAnimationDurationInSeconds => _settings.AnimationDurationInSeconds / 2;

        private IEnumerator LookAtCameraCoroutine()
        {
            while (true)
            {
                _mainTransform.LookAt(_cameraTransform.Position);
                yield return null;
            }
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
            StopCoroutine(_lookAtCameraCoroutine);
            IsUsed = false;
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }

        private Vector3 CalculatePositionToMove(Vector3 cameraPosition)
        {
            var circleCenter = _mainTransform.position;
            var circleNormalVector = (circleCenter - cameraPosition).normalized;

            var circleNormal = Vector3.Cross(circleNormalVector, Vector3.up).normalized;

            var randomAngle = Random.Range(0f, Mathf.PI);
            var randomRadius = Random.Range(_settings.MoveMinimumRadius, _settings.MoveMaximumRadius);

            return circleCenter + randomRadius *
                (Mathf.Cos(randomAngle) * circleNormal + Mathf.Sin(randomAngle) * Vector3.up);
        }
    }
}