using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Common.Settings.Ground_Layer_Mask;
using Enemies.Character;
using Enemies.Controller;
using Enemies.General_Settings;
using Enemies.Look;
using Enemies.Loot_Dropper;
using Enemies.Movement;
using Enemies.Movement.Setup_Data;
using Enemies.Setup.Controller_Setup_Data;
using Enemies.Setup.Settings;
using Enemies.Spawn.Factory;
using Enemies.State_Machine;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Conditions;
using Enemies.Target_Pathfinder;
using Enemies.Target_Pathfinder.Setup_Data;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Trigger;
using Enemies.Visual;
using Enemies.Visual.Dissolve_Effect_Controller;
using Enemies.Visual.Dissolve_Effect_Controller.Settings;
using Factions;
using Pickable_Items.Factory;
using Systems.Scene_Switcher.Current_Game_Level_Information;
using UI.Concrete_Scenes.In_Game.Popup_Text.Factory;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Enemies.Setup
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyController))]
    public class EnemySetup : SetupMonoBehaviourBase, ICoroutineStarter, IEnemySetup
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
        [SerializeField] private ReadonlyTransformGetter _lootSpawnPoint;
        [SerializeField] private Rigidbody _thisRigidbody;
        [SerializeField] private ReadonlyTransformGetter _pointForAiming;
        [SerializeField] private List<Renderer> _renderersForDissolving;

        private EnemyController _controller;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;
        private List<IEnemyTargetTrigger> _externalTargetTriggers;
        private IGeneralEnemySettings _generalEnemySettings;
        private IdHolder _idHolder;
        private IEnemy _initializedEnemy;
        private IPickableItemsFactory _itemsFactory;
        private IPopupHitPointsChangeTextFactory _popupHitPointsChangeTextFactory;
        private IEnemySettings _settings;
        private IInitializableStateEnemyAI[] _states;
        private IInitializableTransitionEnemyAI[] _transitions;
        private IFaction _faction;
        private ISummoner _summoner;
        private IToolsForSummon _toolsForSummon;
        private IGameLevelLootUnlocker _gameLevelLootUnlocker;
        private Collider _collider;
        private IDissolveEffectControllerSettings _dissolveEffectControllerSettings;
        private IDissolveEffectController _dissolveEffectController;

        [Inject]
        private void GetDependencies(IGeneralEnemySettings generalEnemySettings, IPickableItemsFactory itemsFactory,
            IPopupHitPointsChangeTextFactory popupHitPointsChangeTextFactory,
            IGroundLayerMaskSetting groundLayerMaskSetting, IEnemyFactory enemyFactory,
            IGameLevelLootUnlocker gameLevelLootUnlocker,
            IDissolveEffectControllerSettings dissolveEffectControllerSettings)
        {
            _generalEnemySettings = generalEnemySettings;
            _itemsFactory = itemsFactory;
            _popupHitPointsChangeTextFactory = popupHitPointsChangeTextFactory;
            _toolsForSummon = new ToolsForSummon(enemyFactory, groundLayerMaskSetting);
            _gameLevelLootUnlocker = gameLevelLootUnlocker;
            _dissolveEffectControllerSettings = dissolveEffectControllerSettings;
        }

        public IEnemy InitializedEnemy => _initializedEnemy ??= GetComponent<IEnemy>();

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>
            {
                _externalDependenciesInitializationWaiter,
                _thisIKCenterPoint,
                _popupTextHitPointsChangeAppearCenterPoint,
                _lootSpawnPoint,
                _pointForAiming
            };

        public void SetDataForInitialization(IEnemySettings settings, IFaction faction,
            List<IEnemyTargetTrigger> targetTriggers)
        {
            _settings = settings;
            _externalTargetTriggers = targetTriggers;
            _faction = faction;

            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        public void SetDataForInitialization(IEnemySettings settings, IInformationForSummon informationForSummon)
        {
            _summoner = informationForSummon.Summoner;
            SetDataForInitialization(settings, informationForSummon.Faction, informationForSummon.TargetTriggers);
        }

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);

            _initializedEnemy ??= GetComponent<IEnemy>();

            _idHolder = GetComponent<IdHolder>();
            _thisRigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _controller = GetComponent<EnemyController>();

            _states = _enemyStateMachineAI.GetComponentsInChildren<IInitializableStateEnemyAI>();
            _transitions = _enemyStateMachineAI.GetComponentsInChildren<IInitializableTransitionEnemyAI>();

            _dissolveEffectController = new DissolveEffectController(_renderersForDissolving,
                _dissolveEffectControllerSettings, gameObject);
        }

        protected override void Initialize()
        {
            var thisReadonlyRigidbody = new ReadonlyRigidbody(_thisRigidbody);
            var targetPathfinderSetupData = new TargetPathfinderSetupData(thisReadonlyRigidbody, this);
            ITargetPathfinder targetPathfinder =
                _settings.TargetPathfinderProvider.GetImplementationObject(targetPathfinderSetupData);

            var targetFromTriggersSelector = new EnemyTargetFromTriggersSelector(_faction, thisReadonlyRigidbody, this,
                _generalEnemySettings.TargetSelectorUpdateCooldownInSeconds);

            var enemyLook = new EnemyLook(_thisRigidbody.transform, thisReadonlyRigidbody, thisReadonlyRigidbody,
                targetFromTriggersSelector, this, _transformToRotateForIK, _thisIKCenterPoint.ReadonlyTransform,
                _needDistanceFromIKCenterPoint);

            var enemyVisual = new EnemyVisual(_rigBuilder, _characterAnimator, _settings.BaseAnimatorOverrideController,
                _generalEnemySettings.EmptyActionAnimationClip, _dissolveEffectController);

            var movementSetupData = new EnemyMovementSetupData(_thisRigidbody, targetFromTriggersSelector, this,
                targetPathfinder, _summoner, _collider);
            IDisableableEnemyMovement enemyMovement =
                _settings.MovementProvider.GetImplementationObject(movementSetupData);

            IDisableableEnemyCharacter enemyCharacter =
                _settings.CharacterProvider.GetImplementationObject(this, thisReadonlyRigidbody, _summoner);

            var targetTriggers = new List<IEnemyTargetTrigger>(_externalTargetTriggers);
            targetTriggers.AddRange(_localTargetTriggers);
            targetFromTriggersSelector.AddTriggers(targetTriggers);

            ILootDropper lootDropper = _settings.LootDropperProvider.GetImplementation(_itemsFactory,
                _lootSpawnPoint.ReadonlyTransform, _gameLevelLootUnlocker);

            var itemsNeedDisabling = new List<IDisableable>
            {
                enemyMovement, enemyCharacter, targetFromTriggersSelector, enemyLook
            };

            var informationOfSummoner = new InformationForSummon(GetComponent<ISummoner>(), _faction, targetTriggers);

            var baseSetupData = new EnemyControllerSetupData(_enemyStateMachineAI, enemyMovement, itemsNeedDisabling,
                _idHolder, _generalEnemySettings, _popupHitPointsChangeTextFactory, targetFromTriggersSelector,
                enemyLook, _eventInvokerForAnimations, enemyVisual, enemyCharacter,
                _popupTextHitPointsChangeAppearCenterPoint.ReadonlyTransform, lootDropper, _faction,
                _pointForAiming.ReadonlyTransform, informationOfSummoner, _toolsForSummon,
                _lootSpawnPoint.ReadonlyTransform);

            _enemyStateMachineAI.Initialize(_controller);

            foreach (IInitializableStateEnemyAI initializableStateEnemyAI in _states)
            {
                initializableStateEnemyAI.Initialize(_controller);
            }

            foreach (IInitializableTransitionEnemyAI initializableTransitionEnemyAI in _transitions)
            {
                initializableTransitionEnemyAI.Initialize(_controller);
            }

            _controller.Initialize(baseSetupData);
        }
    }
}