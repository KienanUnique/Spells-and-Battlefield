using System.Collections.Generic;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public class DissolveObjectMechanismController : MechanismControllerBase,
        IInitializableDissolveObjectMechanismController
    {
        private IDissolveEffectController _dissolveEffectController;
        private List<Collider> _collidersToDisable;
        private bool _isEnabled;

        public void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> triggers,
            IDissolveEffectController dissolveEffectController, List<Collider> collidersToDisable)
        {
            AddTriggers(triggers);
            _dissolveEffectController = dissolveEffectController;
            _collidersToDisable = collidersToDisable;
            _isEnabled = isEnabledAtStart;
            UpdateCollidersStatus();
            SetInitializedStatus();
        }

        protected override void StartJob()
        {
            _isEnabled = !_isEnabled;

            if (_isEnabled)
            {
                _dissolveEffectController.Appear(HandleDoneJob);
            }
            else
            {
                _dissolveEffectController.Disappear(HandleDoneJob);
            }

            UpdateCollidersStatus();
        }

        private void UpdateCollidersStatus()
        {
            foreach (var colliderToDisable in _collidersToDisable)
            {
                colliderToDisable.enabled = _isEnabled;
            }
        }
    }
}