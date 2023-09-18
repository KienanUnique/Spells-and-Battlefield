using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Settings;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Setup
{
    public class PressurePlateControllerSetup : MechanismsTriggerSetupBase
    {
        [SerializeField] private ColliderTrigger _colliderTrigger;
        [SerializeField] private Transform _visualObject;
        [SerializeField] private IdentifierScriptableObjectBase _identifier;
        private IInitializablePressurePlateController _controller;
        private IPlateSettings _plateSettings;

        [Inject]
        private void GetDependencies(IPlateSettings plateSettings)
        {
            _plateSettings = plateSettings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _controller.Initialize(_identifier, NeedTriggerOneTime, _visualObject, _plateSettings, _colliderTrigger);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePressurePlateController>();
        }
    }
}