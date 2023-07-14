using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Triggers;
using Settings.Puzzles.Triggers.Identifiers;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Plate
{
    public class PressurePlateControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private ColliderTrigger _colliderTrigger;
        [SerializeField] private Transform _visualObject;
        [SerializeField] private IdentifierScriptableObjectBase _identifier;
        private PlateSettings _plateSettings;
        private IInitializablePressurePlateController _controller;

        [Inject]
        private void Construct(PlateSettings plateSettings)
        {
            _plateSettings = plateSettings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePressurePlateController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_identifier, _visualObject, _plateSettings, _colliderTrigger);
        }
    }
}