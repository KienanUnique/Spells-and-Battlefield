using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Character;
using Enemies.Controller;
using Enemies.Look;
using Enemies.Movement;
using Enemies.Movement.Setup_Data;
using Enemies.State_Machine;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Conditions;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Trigger;
using Enemies.Visual;
using Interfaces;
using Pathfinding;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings.Enemies;
using UI.Popup_Text.Factory;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Enemies.Setup
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(EnemyController))]
    public class EnemySetup : SetupMonoBehaviourBase, ICoroutineStarter,
        IEnemyDataForInitializationSettable
    {
        [SerializeField] private EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] private List<EnemyTargetTrigger> _localTargetTriggers;
        [SerializeField] private EventInvokerForActionAnimations _eventInvokerForAnimations;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private Transform _transformToRotateForIK;
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private ReadonlyTransformGetter _thisIKCenterPoint;
        [SerializeField] [Min(0.1f)] private float _needDistanceFromIKCenterPoint = 3f;
        [SerializeField] private ReadonlyTransformGetter _popupTextHitPointsChangeAppearCenterPoint;

        private Seeker _seeker;
        public Rigidbody _thisRigidbody;
        private List<IDisableable> _itemsNeedDisabling;
        private IdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private EnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private List<IEnemyTargetTrigger> _externalTargetTriggers;
        private EnemyLook _enemyLook;
        private IDisableableEnemyMovement _enemyMovement;
        private IDisableableEnemyCharacter _enemyCharacter;
        private EnemyVisual _enemyVisual;
        private IEnemySettings _settings;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;
        private EnemyController _controller;
        private IInitializableStateEnemyAI[] _states;
        private IInitializableTransitionEnemyAI[] _transitions;
        private IPopupHitPointsChangeTextFactory _popupHitPointsChangeTextFactory;
        private IPickableItemDataForCreating _itemToDrop;

        [Inject]
        private void Construct(GeneralEnemySettings generalEnemySettings, IPickableItemsFactory itemsFactory,
            IPopupHitPointsChangeTextFactory popupHitPointsChangeTextFactory)
        {
            _generalEnemySettings = generalEnemySettings;
            _itemsFactory = itemsFactory;
            _popupHitPointsChangeTextFactory = popupHitPointsChangeTextFactory;
        }

        public void SetDataForInitialization(IEnemySettings settings, IPickableItemDataForCreating itemToDrop,
            List<IEnemyTargetTrigger> targetTriggers)
        {
            _settings = settings;
            _externalTargetTriggers = targetTriggers;
            _itemToDrop = itemToDrop;

            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => new List<IInitializable>
            {_externalDependenciesInitializationWaiter, _thisIKCenterPoint};

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);

            _idHolder = GetComponent<IdHolder>();
            _seeker = GetComponent<Seeker>();
            _thisRigidbody = GetComponent<Rigidbody>();
            _controller = GetComponent<EnemyController>();

            _targetFromTriggersSelector = new EnemyTargetFromTriggersSelector();

            _states = _enemyStateMachineAI.GetComponents<IInitializableStateEnemyAI>();
            _transitions = _enemyStateMachineAI.GetComponents<IInitializableTransitionEnemyAI>();
        }

        protected override void Initialize()
        {
            var thisReadonlyRigidbody = new ReadonlyRigidbody(_thisRigidbody);
            _enemyLook = new EnemyLook(_thisRigidbody.transform, thisReadonlyRigidbody, thisReadonlyRigidbody,
                _targetFromTriggersSelector, this, _transformToRotateForIK, _thisIKCenterPoint.ReadonlyTransform,
                _needDistanceFromIKCenterPoint);

            _enemyVisual = new EnemyVisual(_rigBuilder, _characterAnimator, _settings.BaseAnimatorOverrideController,
                _generalEnemySettings.EmptyActionAnimationClip);

            var movementSetupData =
                new EnemyMovementSetupData(_thisRigidbody, _targetFromTriggersSelector, _seeker, this);
            _enemyMovement = _settings.MovementProvider.GetImplementationObject(movementSetupData);

            _enemyCharacter = _settings.CharacterProvider.GetImplementationObject(this);

            _targetFromTriggersSelector.AddTriggers(_externalTargetTriggers);
            _targetFromTriggersSelector.AddTriggers(_localTargetTriggers);

            _itemsNeedDisabling = new List<IDisableable>
            {
                _enemyMovement,
                _enemyCharacter,
                _targetFromTriggersSelector,
                _enemyLook
            };

            var baseSetupData = new EnemyControllerSetupData(
                _enemyStateMachineAI,
                _itemToDrop,
                _enemyMovement,
                _itemsNeedDisabling,
                _idHolder,
                _generalEnemySettings,
                _itemsFactory,
                _targetFromTriggersSelector,
                _enemyLook,
                _eventInvokerForAnimations,
                _enemyVisual,
                _enemyCharacter,
                _popupHitPointsChangeTextFactory,
                _popupTextHitPointsChangeAppearCenterPoint.ReadonlyTransform);

            _enemyStateMachineAI.Initialize(_controller);

            foreach (var initializableStateEnemyAI in _states)
            {
                initializableStateEnemyAI.Initialize(_controller);
            }

            foreach (var initializableTransitionEnemyAI in _transitions)
            {
                initializableTransitionEnemyAI.Initialize(_controller);
            }

            _controller.Initialize(baseSetupData);
        }
    }
}