using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;
using Common.Dissolve_Effect_Controller.Settings;
using UnityEngine;
using Zenject;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public abstract class DissolveObjectMechanismControllerSetupBase : SetupMonoBehaviourBase
    {
        [SerializeField] protected List<Renderer> _renderers;
        [SerializeField] protected List<ColliderWithDisabling> _colliders;
        [SerializeField] protected bool _isEnabledAtStart = true;

        private IDissolveEffectControllerSettings _dissolveEffectControllerSettings;

        [Inject]
        private void GetDependencies(IDissolveEffectControllerSettings dissolveEffectControllerSettings)
        {
            _dissolveEffectControllerSettings = dissolveEffectControllerSettings;
        }

        protected IDissolveEffectController DissolveEffectController =>
            new DissolveEffectController(_renderers, _dissolveEffectControllerSettings, gameObject, !_isEnabledAtStart);

        protected List<IColliderWithDisabling> Colliders => new(_colliders);
    }
}