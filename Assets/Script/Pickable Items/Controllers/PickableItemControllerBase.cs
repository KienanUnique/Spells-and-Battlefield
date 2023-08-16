using System;
using Common;
using Common.Abstract_Bases.Checkers;
using DG.Tweening;
using Interfaces.Pickers;
using Pickable_Items.Setup;
using Pickable_Items.Strategies_For_Pickable_Controller;
using Settings;
using UnityEngine;

namespace Pickable_Items.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PickableItemControllerBase : MonoBehaviour, IPickableItem
    {
        private const float MaxGroundRayDistance = 2f;
        private const float GroundCheckOffsetY = 20f;
        private PickableItemsPickerTrigger _pickerTrigger;
        private GroundChecker _groundChecker;
        private Transform _visualObjectTransform;
        private bool _needFallDown;
        private PickableItemsSettings _pickableItemsSettings;
        private GroundLayerMaskSetting _groundLayerMaskSetting;
        private Rigidbody _rigidbody;
        private ValueWithReactionOnChange<ControllerStates> _currentControllerState;
        private IStrategyForPickableController _strategyForPickableController;
        private GameObject _doTweenLinkGameObject;

        protected void Initialize(IPickableItemControllerBaseSetupData setupData)
        {
            _pickerTrigger = setupData.SetPickerTrigger;
            _groundChecker = setupData.SetGroundChecker;
            _visualObjectTransform = setupData.SetVisualObjectTransform;
            _doTweenLinkGameObject = _visualObjectTransform.gameObject;
            _pickableItemsSettings = setupData.SetPickableItemsSettings;
            _groundLayerMaskSetting = setupData.SetGroundLayerMaskSetting;
            _pickableItemsSettings = setupData.SetPickableItemsSettings;
            _rigidbody = setupData.SetRigidBody;
            _strategyForPickableController = setupData.SetStrategyForPickableController;
            _needFallDown = setupData.SetNeedFallDown;

            SubscribeOnEvents();

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
            _currentControllerState = new ValueWithReactionOnChange<ControllerStates>(ControllerStates.NotInitialized);
        }

        private void OnEnable()
        {
            if (_currentControllerState.Value != ControllerStates.NotInitialized)
            {
                SubscribeOnEvents();
            }
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        protected virtual void SubscribeOnEvents()
        {
            _pickerTrigger.PickerDetected += OnPickerDetected;
            _groundChecker.ContactStateChanged += OnIsGroundedStatusChanged;
            _currentControllerState.AfterValueChanged += OnControllerStateChanged;
        }

        protected virtual void UnsubscribeFromEvents()
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
                    _visualObjectTransform.localScale = Vector3.zero;
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
                .SetLink(_doTweenLinkGameObject)
                .OnComplete(OnPickupAnimationFinished);
        }

        private void StartIdleAnimation()
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;

            var visualObjectSequence = DOTween.Sequence();
            visualObjectSequence.SetLink(_doTweenLinkGameObject);
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
                        _pickableItemsSettings.YAnimationDuration).SetLink(_doTweenLinkGameObject));
                }
            }

            visualObjectSequence.AppendCallback(() => _visualObjectTransform
                .DOMoveY(cashedTransform.position.y + _pickableItemsSettings.AnimationMaximumHeight,
                    _pickableItemsSettings.YAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_pickableItemsSettings.YMovingEase)
                .SetLink(_doTweenLinkGameObject));
            _visualObjectTransform
                .DORotate(new Vector3(0, 360, 0), _pickableItemsSettings.RotateAnimationDuration,
                    RotateMode.FastBeyond360)
                .SetEase(_pickableItemsSettings.RotatingEase)
                .SetLoops(-1, LoopType.Restart)
                .SetLink(_doTweenLinkGameObject);
        }


        private void PlayAppearAnimation()
        {
            _visualObjectTransform.DOScale(Vector3.one, _pickableItemsSettings.AppearScaleAnimationDuration)
                .SetEase(_pickableItemsSettings.SizeChangeEase)
                .SetLink(_doTweenLinkGameObject);
        }


        private void OnPickupAnimationFinished()
        {
            _visualObjectTransform.DOKill();
            Destroy(this.gameObject);
        }
    }
}