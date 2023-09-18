using System.Collections.Generic;
using DG.Tweening;
using ModestTree;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Settings;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Setup;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate
{
    [RequireComponent(typeof(PressurePlateControllerSetup))]
    public class PressurePlateController : MechanismsTriggerBase, IInitializablePressurePlateController
    {
        private IColliderTrigger _colliderTrigger;
        private IIdentifier _identifier;
        private IPlateSettings _plateSettings;
        private Transform _plateTransform;
        private List<Collider> _requiredObjectsOnPanel;
        private bool _needTriggerOneTime;

        public void Initialize(IIdentifier identifier, bool needTriggerOneTime, Transform plateTransform,
            IPlateSettings plateSettings, IColliderTrigger colliderTrigger)
        {
            _identifier = identifier;
            _requiredObjectsOnPanel = new List<Collider>();
            _plateTransform = plateTransform;
            _plateSettings = plateSettings;
            _colliderTrigger = colliderTrigger;
            _needTriggerOneTime = needTriggerOneTime;
            SetInitializedStatus();
            ProcessPressingUp();
        }

        protected override bool NeedTriggerOneTime => _needTriggerOneTime;

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
            if (!_identifier.IsObjectOfRequiredType(other))
            {
                return;
            }

            if (_requiredObjectsOnPanel.IsEmpty())
            {
                ProcessPressingDown();
            }

            _requiredObjectsOnPanel.Add(other);
        }

        private void OnColliderTriggerExit(Collider other)
        {
            if (!_identifier.IsObjectOfRequiredType(other) || !_requiredObjectsOnPanel.Contains(other))
            {
                return;
            }

            _requiredObjectsOnPanel.Remove(other);
            if (_requiredObjectsOnPanel.IsEmpty())
            {
                ProcessPressingUp();
            }
        }

        private void ProcessPressingDown()
        {
            TryInvokeTriggerEvent();
            _plateTransform.DOComplete();
            _plateTransform.DOScaleY(_plateSettings.PressedScaleY, _plateSettings.AnimationDuration)
                           .SetEase(_plateSettings.AnimationEase)
                           .SetLink(gameObject);
            float scaleDeltaPositionY = (_plateSettings.UnpressedScaleY - _plateSettings.PressedScaleY) / -2;
            _plateTransform.DOLocalMoveY(scaleDeltaPositionY, _plateSettings.AnimationDuration)
                           .SetEase(_plateSettings.AnimationEase)
                           .SetLink(gameObject);
        }

        private void ProcessPressingUp()
        {
            _plateTransform.DOComplete();
            _plateTransform.DOScaleY(_plateSettings.UnpressedScaleY, _plateSettings.AnimationDuration)
                           .SetEase(_plateSettings.AnimationEase)
                           .SetLink(gameObject);
            _plateTransform.DOLocalMoveY(0, _plateSettings.AnimationDuration)
                           .SetEase(_plateSettings.AnimationEase)
                           .SetLink(gameObject);
        }
    }
}