using System.Collections.Generic;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public abstract class DissolveObjectMechanismControllerBase : MechanismControllerBase
    {
        private IDissolveEffectController _dissolveEffectController;
        private List<IColliderWithDisabling> _collidersToDisable;
        protected bool IsEnabled { get; private set; }

        public void InitializeBase(bool isEnabledAtStart, IDissolveEffectController dissolveEffectController,
            List<IColliderWithDisabling> collidersToDisable)
        {
            _dissolveEffectController = dissolveEffectController;
            _collidersToDisable = collidersToDisable;
            ChangeState(isEnabledAtStart);
            SetInitializedStatus();
        }

        protected void ChangeState(bool newIsEnabled)
        {
            if (newIsEnabled == IsEnabled)
            {
                return;
            }

            IsEnabled = newIsEnabled;
            if (newIsEnabled)
            {
                _dissolveEffectController.Appear(HandleDoneJob);
                foreach (var colliderToDisable in _collidersToDisable)
                {
                    colliderToDisable.EnableCollider();
                }
            }
            else
            {
                _dissolveEffectController.Disappear(HandleDoneJob);
                foreach (var colliderToDisable in _collidersToDisable)
                {
                    colliderToDisable.DisableCollider();
                }
            }
        }
    }
}