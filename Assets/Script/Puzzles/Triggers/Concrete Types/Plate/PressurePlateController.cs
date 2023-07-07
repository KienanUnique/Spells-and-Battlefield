using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using DG.Tweening;
using ModestTree;
using Puzzles.Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Triggers;
using Settings.Puzzles.Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Triggers.Concrete_Types.Plate
{
    [RequireComponent(typeof(PressurePlateControllerSetup))]
    public class PressurePlateController : TriggerBase, IInitializablePressurePlateController
    {
        private IIdentifier _identifier;
        private List<Collider> _requiredObjectsOnPanel;
        private Transform _plateTransform;
        private PlateSettings _plateSettings;
        private IColliderTrigger _colliderTrigger;

        public void Initialize(IIdentifier identifier, Transform plateTransform, PlateSettings plateSettings,
            IColliderTrigger colliderTrigger)
        {
            _identifier = identifier;
            _requiredObjectsOnPanel = new List<Collider>();
            _plateTransform = plateTransform;
            _plateSettings = plateSettings;
            _colliderTrigger = colliderTrigger;
            SetInitializedStatus();
            ProcessPressingUp();
        }

        public override event Action Triggered;

        protected override void SubscribeOnEvents()
        {
            _colliderTrigger.TriggerEnter += OnColliderTriggerEnter;
            _colliderTrigger.TriggerExit += OnColliderTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            _colliderTrigger.TriggerEnter -= OnColliderTriggerEnter;
            _colliderTrigger.TriggerExit -= OnColliderTriggerExit;
        }

        private void OnColliderTriggerEnter(Collider other)
        {
            if (!_identifier.IsObjectOfRequiredType(other)) return;
            if (_requiredObjectsOnPanel.IsEmpty())
            {
                ProcessPressingDown();
            }

            _requiredObjectsOnPanel.Add(other);
        }

        private void OnColliderTriggerExit(Collider other)
        {
            if (!_identifier.IsObjectOfRequiredType(other) || !_requiredObjectsOnPanel.Contains(other)) return;
            _requiredObjectsOnPanel.Remove(other);
            if (_requiredObjectsOnPanel.IsEmpty())
            {
                ProcessPressingUp();
            }
        }

        private void ProcessPressingDown()
        {
            Triggered?.Invoke();
            _plateTransform.DOComplete();
            _plateTransform.DOScaleY(_plateSettings.PressedScaleY, _plateSettings.AnimationDuration)
                .SetEase(_plateSettings.AnimationEase).SetLink(gameObject);
            var scaleDeltaPositionY = (_plateSettings.UnpressedScaleY - _plateSettings.PressedScaleY) / -2;
            _plateTransform.DOLocalMoveY(scaleDeltaPositionY, _plateSettings.AnimationDuration)
                .SetEase(_plateSettings.AnimationEase).SetLink(gameObject);
        }

        private void ProcessPressingUp()
        {
            _plateTransform.DOComplete();
            _plateTransform.DOScaleY(_plateSettings.UnpressedScaleY, _plateSettings.AnimationDuration)
                .SetEase(_plateSettings.AnimationEase).SetLink(gameObject);
            _plateTransform.DOLocalMoveY(0, _plateSettings.AnimationDuration)
                .SetEase(_plateSettings.AnimationEase).SetLink(gameObject);
        }
    }
}