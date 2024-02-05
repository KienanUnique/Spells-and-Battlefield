using System.Collections.Generic;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public class DissolveObjectMechanismController : MechanismControllerBase,
        IInitializableDissolveObjectMechanismController
    {
        private IDissolveEffectController _dissolveEffectController;
        private List<IColliderWithDisabling> _collidersToDisable;
        private bool _isEnabled;

        public void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> triggers,
            IDissolveEffectController dissolveEffectController, List<IColliderWithDisabling> collidersToDisable)
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
            if (_isEnabled)
            {
                foreach (var colliderToDisable in _collidersToDisable)
                {
                    colliderToDisable.EnableCollider();
                }
            }
            else
            {
                foreach (var colliderToDisable in _collidersToDisable)
                {
                    colliderToDisable.DisableCollider();
                }
            }
        }
    }
}