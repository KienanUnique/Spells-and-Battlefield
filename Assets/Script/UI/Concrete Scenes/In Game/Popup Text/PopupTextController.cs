using System.Collections;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using DG.Tweening;
using TMPro;
using UI.Concrete_Scenes.In_Game.Popup_Text.Data_For_Activation;
using UI.Concrete_Scenes.In_Game.Popup_Text.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Concrete_Scenes.In_Game.Popup_Text
{
    public class PopupTextController :
        InitializableMonoBehaviourWithObjectPoolingBase<IPopupTextControllerDataForActivation>,
        IInitializablePopupTextController,
        IPopupTextController
    {
        private IReadonlyTransform _cameraTransform;
        private Coroutine _lookAtCameraCoroutine;
        private Transform _mainTransform;
        private IPopupTextSettings _settings;
        private TMP_Text _textComponent;

        public void Initialize(TMP_Text textComponent, Transform mainTransform, IPopupTextSettings settings,
            IReadonlyTransform cameraTransform)
        {
            _textComponent = textComponent;
            _mainTransform = mainTransform;
            _settings = settings;
            _cameraTransform = cameraTransform;
            gameObject.SetActive(false);
            SetInitializedStatus();
        }

        private float HalfAnimationDurationInSeconds => _settings.AnimationDurationInSeconds / 2;

        public override void Activate(IPopupTextControllerDataForActivation dataForActivation)
        {
            base.Activate(dataForActivation);
            _textComponent.text = dataForActivation.TextToShow;

            _mainTransform.localScale = Vector3.zero;
            _mainTransform.SetPositionAndRotation(dataForActivation.SpawnPosition, dataForActivation.SpawnRotation);
            gameObject.SetActive(true);

            _lookAtCameraCoroutine = StartCoroutine(LookAtCameraCoroutine());

            if (_mainTransform == null)
            {
            }

            Sequence needSequence = DOTween.Sequence();
            needSequence.ApplyCustomSetupForUI(_mainTransform.gameObject);
            needSequence.Append(_mainTransform.DOScale(Vector3.one, HalfAnimationDurationInSeconds)
                                              .SetEase(_settings.ScaleEase));
            needSequence.Append(_mainTransform.DOScale(Vector3.zero, HalfAnimationDurationInSeconds)
                                              .SetEase(_settings.ScaleEase));
            needSequence.OnComplete(OnPopupAnimationComplete);

            _mainTransform
                .DOMove(CalculatePositionToMove(_cameraTransform.Position), _settings.AnimationDurationInSeconds)
                .SetEase(_settings.MovementEase)
                .ApplyCustomSetupForUI(_mainTransform.gameObject);
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        private IEnumerator LookAtCameraCoroutine()
        {
            while (true)
            {
                _mainTransform.LookAt(_cameraTransform.Position);
                yield return null;
            }
        }

        private void OnPopupAnimationComplete()
        {
            _mainTransform.DOKill();
            StopCoroutine(_lookAtCameraCoroutine);
            gameObject.SetActive(false);
            Deactivate();
        }

        private Vector3 CalculatePositionToMove(Vector3 cameraPosition)
        {
            Vector3 circleCenter = _mainTransform.position;
            Vector3 circleNormalVector = (circleCenter - cameraPosition).normalized;

            Vector3 circleNormal = Vector3.Cross(circleNormalVector, Vector3.up).normalized;

            float randomAngle = Random.Range(0f, Mathf.PI);
            float randomRadius = Random.Range(_settings.MoveMinimumRadius, _settings.MoveMaximumRadius);

            return circleCenter +
                   randomRadius * (Mathf.Cos(randomAngle) * circleNormal + Mathf.Sin(randomAngle) * Vector3.up);
        }
    }
}