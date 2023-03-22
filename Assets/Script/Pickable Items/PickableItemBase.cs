﻿using System;
using DG.Tweening;
using Interfaces;
using Triggers;
using UnityEngine;

namespace Pickable_Items
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PickableItemBase<TStoredObject> : MonoBehaviour
    {
        [SerializeField] private float _animationMinimumHeight = 1.6f;
        [SerializeField] private float _animationMaximumHeight = 2.6f;
        [SerializeField] private float _yAnimationDuration = 0.6f;
        [SerializeField] private float _rotateAnimationDuration = 1;
        [SerializeField] private float _appearScaleAnimationDuration = 3;
        [SerializeField] private float _disappearScaleAnimationDuration = 1;
        [SerializeField] private float _dropForce = 10f;
        [SerializeField] private bool _needFallDown = true;
        [SerializeField] private DroppedItemsPickerTrigger _pickerTrigger;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _visualObjectTransform;

        private Rigidbody _rigidbody;
        private ItemStates _currentState;
        private TStoredObject _storedObject;
        private const float MaxGroundRayDistance = 2f;
        private const float GroundCheckOffsetY = 20f;

        protected TStoredObject StoredObject
        {
            get => _storedObject;
            set
            {
                _storedObject = value;
                _currentState = _needFallDown ? ItemStates.StartFalling : ItemStates.StartIdle;
                AppearItem();
            }
        }

        public void DropItem(TStoredObject storedObject, Vector3 direction)
        {
            StoredObject = storedObject;
            _rigidbody.AddForce(_dropForce * direction, ForceMode.Impulse);
        }

        protected abstract void SpecialPickUpAction(IDroppedItemsPicker component);
        protected abstract void SpecialAppearAction();
        protected abstract void SpecialStartAction();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _currentState = ItemStates.NotInitialized;
        }

        private void OnEnable()
        {
            _pickerTrigger.RequiredObjectEnteringDetected += OnPickerDetected;
        }

        private void OnDisable()
        {
            _pickerTrigger.RequiredObjectEnteringDetected -= OnPickerDetected;
        }

        private void Start()
        {
            SpecialStartAction();
            _visualObjectTransform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (_currentState == ItemStates.StartFalling)
            {
                _rigidbody.useGravity = true;
                _currentState = ItemStates.Falling;
            }

            if (_currentState == ItemStates.Falling && _groundChecker.IsGrounded)
            {
                _currentState = ItemStates.StartIdle;
            }

            if (_currentState == ItemStates.StartIdle)
            {
                _rigidbody.useGravity = false;
                _rigidbody.velocity = Vector3.zero;

                var visualObjectSequence = DOTween.Sequence();
                var localTransform = transform;
                if (_needFallDown)
                {
                    var startRayCheckPosition = localTransform.position;
                    startRayCheckPosition.y += GroundCheckOffsetY;
                    if (Physics.Raycast(startRayCheckPosition, Vector3.down,
                            out var hitGround, MaxGroundRayDistance + GroundCheckOffsetY, _groundMask))
                    {
                        visualObjectSequence.Append(_visualObjectTransform.DOMoveY(
                            hitGround.point.y + _animationMinimumHeight, _yAnimationDuration));
                    }
                }

                visualObjectSequence.AppendCallback(() => _visualObjectTransform
                    .DOMoveY(localTransform.position.y + _animationMaximumHeight, _yAnimationDuration)
                    .SetLoops(-1, LoopType.Yoyo));
                _visualObjectTransform
                    .DORotate(new Vector3(0, 360, 0), _rotateAnimationDuration, RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Restart);
                _currentState = ItemStates.Idle;
            }
        }

        private void AppearItem()
        {
            if (_currentState == ItemStates.NotInitialized)
            {
                throw new SpellObjectWasAlreadyShown();
            }

            SpecialAppearAction();
            _visualObjectTransform.DOScale(new Vector3(1, 1, 1), _appearScaleAnimationDuration);
        }

        private void OnPickerDetected(IDroppedItemsPicker picker)
        {
            SpecialPickUpAction(picker);
            _visualObjectTransform.DOScale(Vector3.zero, _disappearScaleAnimationDuration)
                .OnComplete(OnPickupAnimationFinished);
            if (_currentState != ItemStates.Idle)
            {
                _currentState = ItemStates.StartIdle;
            }
        }

        private void OnPickupAnimationFinished()
        {
            _visualObjectTransform.DOKill();
            Destroy(this.gameObject);
        }

        private enum ItemStates
        {
            NotInitialized,
            StartFalling,
            Falling,
            StartIdle,
            Idle
        }

        private class SpellObjectWasAlreadyShown : Exception
        {
            public SpellObjectWasAlreadyShown() : base("Spell object was already shown")
            {
            }
        }
    }
}