using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Pass_Through_Zone
{
    public class PassThroughZoneControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private ColliderTrigger _colliderTrigger;
        [SerializeField] private IdentifierScriptableObjectBase _identifier;
        private IInitializablePassThroughZoneController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _controller.Initialize(_identifier, _colliderTrigger);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePassThroughZoneController>();
        }
    }
}