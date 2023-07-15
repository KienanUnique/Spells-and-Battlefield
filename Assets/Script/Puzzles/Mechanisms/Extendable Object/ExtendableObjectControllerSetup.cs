using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Puzzles.Mechanisms_Triggers;
using Settings.Puzzles.Mechanisms;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Extendable_Object
{
    public class ExtendableObjectControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _triggers;
        [SerializeField] private ExtendableObjectState _startState;
        [SerializeField] private float _animationDuration;
        [SerializeField] private Transform _objectToExtend;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        private IInitializableExtendableObjectController _controller;
        private ExtendableObjectsSettings _settings;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(_triggers);

        [Inject]
        private void Construct(ExtendableObjectsSettings settings)
        {
            _settings = settings;
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableExtendableObjectController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(new List<IMechanismsTrigger>(_triggers), _startState, _startPoint.position,
                _endPoint.position, _animationDuration, _objectToExtend, _settings);
        }
    }
}