using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Dissolve_Effect_Controller;
using Common.Dissolve_Effect_Controller.Settings;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Dissolve_Object
{
    public class DissolveObjectMechanismControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<Renderer> _renderers;
        [SerializeField] private List<Collider> _colliders;
        [SerializeField] private List<MechanismsTriggerBase> _triggers;
        [SerializeField] private bool _isEnabledAtStart = true;

        private IInitializableDissolveObjectMechanismController _controller;
        private IDissolveEffectControllerSettings _dissolveEffectControllerSettings;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(_triggers);

        [Inject]
        private void GetDependencies(IDissolveEffectControllerSettings dissolveEffectControllerSettings)
        {
            _dissolveEffectControllerSettings = dissolveEffectControllerSettings;
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableDissolveObjectMechanismController>();
        }

        protected override void Initialize()
        {
            var dissolveController = new DissolveEffectController(_renderers, _dissolveEffectControllerSettings,
                gameObject, !_isEnabledAtStart);
            _controller.Initialize(_isEnabledAtStart, new List<IMechanismsTrigger>(_triggers), dissolveController,
                _colliders);
        }
    }
}