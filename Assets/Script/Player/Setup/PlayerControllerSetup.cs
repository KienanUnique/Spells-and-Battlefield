using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Abstract_Bases.Checkers.Wall_Checker;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Id_Holder;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Common.Settings.Ground_Layer_Mask;
using Enemies.Spawn.Factory;
using Enemies.Trigger;
using Player.Camera_Effects;
using Player.Camera_Effects.Camera_Field_Of_View_Calculator;
using Player.Camera_Effects.Camera_Particle_System_Controller;
using Player.Camera_Effects.Camera_Rotator;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Settings;
using Player.Spell_Manager;
using Player.Spell_Manager.Spells_Selector;
using Player.Visual;
using Spells;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using Spells.Spell_Handlers.Continuous;
using Spells.Spell_Handlers.Instant;
using Spells.Spell_Types_Settings;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Player.Setup
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerControllerSetup : SetupMonoBehaviourBase, ICoroutineStarter
    {
        [Header("Camera")] [SerializeField] private ReadonlyTransformGetter _cameraFollowObject;
        [SerializeField] private Transform _objectToRotateHorizontally;
        [SerializeField] private GameObject _cameraEffectsGameObject;
        [SerializeField] private Camera _camera;
        [SerializeField] private ParticleSystem _windTrailsParticleSystem;

        [Header("Animations")]
        [SerializeField]
        private RigBuilder _rigBuilder;

        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private EventInvokerForActionAnimations _eventInvokerForAnimations;

        [Header("Checkers")] [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private WallChecker _wallChecker;

        [Header("Spells")] [SerializeField] private List<SpellScriptableObjectBase> _startTestSpells;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObject;
        private IReadonlyTransform _cameraTransform;
        private IIdHolder _idHolder;
        private List<IDisableable> _itemsNeedDisabling;

        [Header("Other")] [SerializeField] private ReadonlyTransformGetter _pointForAiming;
        [SerializeField] private ReadonlyTransformGetter _upperPointForSummonedEnemiesPositionCalculating;
        [SerializeField] private List<EnemyTargetTrigger> _triggersForSummonedEnemies;

        private IPlayerCameraEffects _playerCameraEffects;
        private ICaster _playerCaster;
        private IPlayerCharacter _playerCharacter;
        private IPlayerInput _playerInput;
        private IPlayerVisual _playerVisual;
        private IPlayerSettings _settings;
        private ISpellObjectsFactory _spellObjectsFactory;
        private ISpellTypesSetting _spellTypesSetting;
        private Rigidbody _thisRigidbody;
        private IToolsForSummon _toolsForSummon;

        [Inject]
        private void GetDependencies(IPlayerInput playerInput, IPlayerSettings settings,
            ISpellObjectsFactory spellObjectsFactory, ISpellTypesSetting spellTypesSetting,
            IGroundLayerMaskSetting groundLayerMaskSetting, IEnemyFactory enemyFactory)
        {
            _playerInput = playerInput;
            _settings = settings;
            _spellObjectsFactory = spellObjectsFactory;
            _spellTypesSetting = spellTypesSetting;
            _toolsForSummon = new ToolsForSummon(enemyFactory, groundLayerMaskSetting);
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>
            {
                _cameraFollowObject,
                _spellSpawnObject,
                _wallChecker,
                _groundChecker,
                _upperPointForSummonedEnemiesPositionCalculating
            };

        protected override void Prepare()
        {
            _playerCaster = GetComponent<ICaster>();
            _idHolder = GetComponent<IdHolder>();
            _thisRigidbody = GetComponent<Rigidbody>();

            var playerCharacter = new PlayerCharacter(this, _settings.Character, gameObject);

            _itemsNeedDisabling = new List<IDisableable> {playerCharacter};

            _playerCharacter = playerCharacter;

            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator, _settings.Visual);
            var cameraFieldOfViewController =
                new PlayerCameraFieldOfViewController(_settings.CameraEffects, _camera, this);
            var cameraRotationController =
                new PlayerCameraRotationController(_cameraEffectsGameObject.transform, _settings.CameraEffects);
            var particleSystemController =
                new PlayerCameraParticleSystemController(_settings.CameraEffects, _windTrailsParticleSystem);
            _playerCameraEffects = new PlayerCameraEffects(cameraRotationController, cameraFieldOfViewController,
                particleSystemController);

            _cameraTransform = new ReadonlyTransform(_camera.transform);
        }

        protected override void Initialize()
        {
            var playerMovementValuesCalculator =
                new PlayerMovementValuesCalculator(_settings.Movement, new ReadonlyRigidbody(_thisRigidbody), this);
            var playerMovement = new PlayerMovement(_thisRigidbody, _settings.Movement, _groundChecker, _wallChecker,
                playerMovementValuesCalculator, this);

            var playerLook = new PlayerLook(_camera, _cameraFollowObject.ReadonlyTransform, _objectToRotateHorizontally,
                _settings.Look);

            var continuousSpellHandler = new ContinuousSpellHandlerImplementation(_playerCaster, _spellObjectsFactory,
                _spellSpawnObject.ReadonlyTransform, playerLook);
            var instantSpellHandler = new InstantSpellHandlerImplementation(_playerCaster, _spellObjectsFactory,
                _spellSpawnObject.ReadonlyTransform, playerLook);
            var spellsSelector =
                new PlayerSpellsSelectorForSpellManager(new List<ISpell>(_startTestSpells), _spellTypesSetting);
            var playerSpellsManager =
                new PlayerSpellsManager(continuousSpellHandler, instantSpellHandler, spellsSelector);
            playerSpellsManager.AddSpell(_spellTypesSetting.LastChanceSpellType,
                _settings.SpellManager.LastChanceSpell);

            _itemsNeedDisabling.Add(playerMovement);
            _itemsNeedDisabling.Add(playerSpellsManager);
            _itemsNeedDisabling.Add(continuousSpellHandler);
            _itemsNeedDisabling.Add(instantSpellHandler);
            _itemsNeedDisabling.Add(spellsSelector);

            var controllerToSetup = GetComponent<IInitializablePlayerController>();
            var informationOfSummoner = new InformationForSummon(GetComponent<ISummoner>(), _settings.Faction,
                new List<IEnemyTargetTrigger>(_triggersForSummonedEnemies));
            var setupData = new PlayerControllerSetupData(_eventInvokerForAnimations, _playerCameraEffects,
                _playerVisual, _playerCharacter, playerSpellsManager, _playerInput, playerMovement, playerLook,
                _idHolder, _itemsNeedDisabling, _cameraTransform, _pointForAiming.ReadonlyTransform, _settings.Faction,
                informationOfSummoner, _toolsForSummon,
                _upperPointForSummonedEnemiesPositionCalculating.ReadonlyTransform);
            controllerToSetup.Initialize(setupData);
        }
    }
}