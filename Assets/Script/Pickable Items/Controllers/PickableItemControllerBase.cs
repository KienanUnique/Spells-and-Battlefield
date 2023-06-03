using System;
using Common;
using Common.Abstract_Bases.Checkers;
using DG.Tweening;
using Interfaces.Pickers;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;
using Zenject;

namespace Pickable_Items.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PickableItemControllerBase : MonoBehaviour, IPickableItem
    {
        private const float MaxGroundRayDistance = 2f;
        private const float GroundCheckOffsetY = 20f;
        [SerializeField] private PickableItemsPickerTrigger _pickerTrigger;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private Transform _visualObjectTransform;
        private bool _needFallDown;
        private PickableItemsSettings _pickableItemsSettings;
        private GroundLayerMaskSetting _groundLayerMaskSetting;
        private Rigidbody _rigidbody;
        private ValueWithReactionOnChange<ControllerStates> _currentControllerState;
        private IStrategyForPickableController _strategyForPickableController;

        [Inject]
        private void Construct(GroundLayerMaskSetting groundLayerMaskSetting,
            PickableItemsSettings pickableItemsSettings)
        {
            _groundLayerMaskSetting = groundLayerMaskSetting;
            _pickableItemsSettings = pickableItemsSettings;
        }

        protected void Initialize(IStrategyForPickableController strategyForPickableController, bool needFallDown)
        {
            _strategyForPickableController = strategyForPickableController;
            _needFallDown = needFallDown;

            _currentControllerState.Value = ControllerStates.Initialized;
        }

        private enum ControllerStates
        {
            NotInitialized,
            Initialized,
            Falling,
            Idle,
            PickedUp
        }

        public void DropItemTowardsDirection(Vector3 direction)
        {
            _rigidbody.AddForce(_pickableItemsSettings.DropForce * direction, ForceMode.Impulse);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _currentControllerState = new ValueWithReactionOnChange<ControllerStates>(ControllerStates.NotInitialized);
            _visualObjectTransform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            _pickerTrigger.PickerDetected += OnPickerDetected;
            _groundChecker.ContactStateChanged += OnIsGroundedStatusChanged;
            _currentControllerState.AfterValueChanged += OnControllerStateChanged;
        }

        private void OnDisable()
        {
            _pickerTrigger.PickerDetected -= OnPickerDetected;
            _groundChecker.ContactStateChanged -= OnIsGroundedStatusChanged;
            _currentControllerState.AfterValueChanged -= OnControllerStateChanged;
        }

        private void OnControllerStateChanged(ControllerStates newState)
        {
            switch (newState)
            {
                case ControllerStates.Initialized:
                    PlayAppearAnimation();
                    _currentControllerState.Value = _needFallDown ? ControllerStates.Falling : ControllerStates.Idle;
                    break;
                case ControllerStates.Falling:
                    _rigidbody.useGravity = true;
                    break;
                case ControllerStates.Idle:
                    StartIdleAnimation();
                    break;
                case ControllerStates.PickedUp:
                    PlayDisappearAnimationAndDestroy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void OnIsGroundedStatusChanged(bool isGrounded)
        {
            if (isGrounded && _currentControllerState.Value == ControllerStates.Falling)
            {
                _currentControllerState.Value = ControllerStates.Idle;
            }
        }

        private void OnPickerDetected(IPickableItemsPicker picker)
        {
            if (!_strategyForPickableController.CanBePickedUpByThisPeeker(picker)) return;
            _strategyForPickableController.HandlePickUp(picker);
            _currentControllerState.Value = ControllerStates.PickedUp;
        }

        private void PlayDisappearAnimationAndDestroy()
        {
            _visualObjectTransform.DOScale(Vector3.zero, _pickableItemsSettings.DisappearScaleAnimationDuration)
                .SetEase(_pickableItemsSettings.SizeChangeEase)
                .SetLink(gameObject)
                .OnComplete(OnPickupAnimationFinished);
        }

        private void StartIdleAnimation()
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;

            var visualObjectSequence = DOTween.Sequence();
            var cashedTransform = transform;
            if (_needFallDown)
            {
                var startRayCheckPosition = cashedTransform.position;
                startRayCheckPosition.y += GroundCheckOffsetY;
                if (Physics.Raycast(startRayCheckPosition, Vector3.down,
                        out var hitGround, MaxGroundRayDistance + GroundCheckOffsetY,
                        _groundLayerMaskSetting.Mask))
                {
                    visualObjectSequence.Append(_visualObjectTransform.DOMoveY(
                        hitGround.point.y + _pickableItemsSettings.AnimationMinimumHeight,
                        _pickableItemsSettings.YAnimationDuration));
                }
            }

            visualObjectSequence.AppendCallback(() => _visualObjectTransform
                .DOMoveY(cashedTransform.position.y + _pickableItemsSettings.AnimationMaximumHeight,
                    _pickableItemsSettings.YAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_pickableItemsSettings.YMovingEase)
                .SetLink(gameObject));
            _visualObjectTransform
                .DORotate(new Vector3(0, 360, 0), _pickableItemsSettings.RotateAnimationDuration,
                    RotateMode.FastBeyond360)
                .SetEase(_pickableItemsSettings.RotatingEase)
                .SetLoops(-1, LoopType.Restart)
                .SetLink(gameObject);
        }


        private void PlayAppearAnimation()
        {
            _visualObjectTransform.DOScale(Vector3.one, _pickableItemsSettings.AppearScaleAnimationDuration)
                .SetEase(_pickableItemsSettings.SizeChangeEase)
                .SetLink(gameObject);
        }


        private void OnPickupAnimationFinished()
        {
            _visualObjectTransform.DOKill();
            Destroy(this.gameObject);
        }
    }
}