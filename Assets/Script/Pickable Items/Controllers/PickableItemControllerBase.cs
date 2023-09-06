using System;
using Common;
using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Settings.Ground_Layer_Mask;
using DG.Tweening;
using Interfaces.Pickers;
using Pickable_Items.Settings;
using Pickable_Items.Setup;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PickableItemControllerBase : InitializableMonoBehaviourBase, IPickableItem
    {
        private const float MaxGroundRayDistance = 2f;
        private const float GroundCheckOffsetY = 20f;

        private readonly ValueWithReactionOnChange<ControllerStates> _currentControllerState =
            new ValueWithReactionOnChange<ControllerStates>(ControllerStates.NonInitialized);

        private GameObject _doTweenLinkGameObject;
        private GroundChecker _groundChecker;
        private IGroundLayerMaskSetting _groundLayerMaskSetting;
        private bool _needFallDown;
        private IPickableItemsSettings _pickableItemsSettings;

        private PickableItemsPickerTrigger _pickerTrigger;
        private Rigidbody _rigidbody;
        private IStrategyForPickableController _strategyForPickableController;
        private Transform _visualObjectTransform;

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
            SetInitializedStatus();

            PlayAppearAnimation();
            _visualObjectTransform.localScale = Vector3.zero;
            _currentControllerState.Value = _needFallDown ? ControllerStates.Falling : ControllerStates.Idle;
        }

        private enum ControllerStates
        {
            NonInitialized,
            Falling,
            Idle,
            PickedUp
        }

        public void DropItemTowardsDirection(Vector3 direction)
        {
            _rigidbody.AddForce(_pickableItemsSettings.DropForce * direction, ForceMode.Impulse);
        }

        protected override void SubscribeOnEvents()
        {
            _pickerTrigger.PickerDetected += OnPickerDetected;
            _groundChecker.ContactStateChanged += OnIsGroundedStatusChanged;
            _currentControllerState.AfterValueChanged += OnControllerStateChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _pickerTrigger.PickerDetected -= OnPickerDetected;
            _groundChecker.ContactStateChanged -= OnIsGroundedStatusChanged;
            _currentControllerState.AfterValueChanged -= OnControllerStateChanged;
        }

        private void OnControllerStateChanged(ControllerStates newState)
        {
            switch (newState)
            {
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
            if (!_strategyForPickableController.CanBePickedUpByThisPeeker(picker))
            {
                return;
            }

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

            Sequence visualObjectSequence = DOTween.Sequence();
            visualObjectSequence.SetLink(_doTweenLinkGameObject);
            Transform cashedTransform = transform;
            if (_needFallDown)
            {
                Vector3 startRayCheckPosition = cashedTransform.position;
                startRayCheckPosition.y += GroundCheckOffsetY;
                if (Physics.Raycast(startRayCheckPosition, Vector3.down, out RaycastHit hitGround,
                        MaxGroundRayDistance + GroundCheckOffsetY, _groundLayerMaskSetting.Mask))
                {
                    visualObjectSequence.Append(_visualObjectTransform.DOMoveY(
                                                                          hitGround.point.y +
                                                                          _pickableItemsSettings.AnimationMinimumHeight,
                                                                          _pickableItemsSettings.YAnimationDuration)
                                                                      .SetLink(_doTweenLinkGameObject));
                }
            }

            visualObjectSequence.AppendCallback(() => _visualObjectTransform
                                                      .DOMoveY(
                                                          cashedTransform.position.y +
                                                          _pickableItemsSettings.AnimationMaximumHeight,
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
            Destroy(gameObject);
        }
    }
}