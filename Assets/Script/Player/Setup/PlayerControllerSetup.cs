using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Abstract_Bases.Checkers.Wall_Checker;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Readonly_Transform;
using Interfaces;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Settings;
using Player.Spell_Manager;
using Player.Visual;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using Spells.Spell_Types_Settings;
using Systems.Input_Manager;
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

        [Header("Animations")]
        [SerializeField]
        private RigBuilder _rigBuilder;

        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private EventInvokerForActionAnimations _eventInvokerForAnimations;

        [Header("Checkers")] [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private WallChecker _wallChecker;

        [Header("Spells")] [SerializeField] private List<SpellScriptableObject> _startTestSpells;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObject;
        private IReadonlyTransform _cameraTransform;
        private IIdHolder _idHolder;
        private List<IDisableable> _itemsNeedDisabling;

        [Header("Other")] [SerializeField] private ReadonlyTransformGetter _pointForAiming;

        private IPlayerCameraEffects _playerCameraEffects;
        private ICaster _playerCaster;
        private IPlayerCharacter _playerCharacter;
        private IPlayerInput _playerInput;
        private IPlayerVisual _playerVisual;
        private IPlayerSettings _settings;
        private ISpellObjectsFactory _spellObjectsFactory;
        private ISpellTypesSetting _spellTypesSetting;
        private Rigidbody _thisRigidbody;

        [Inject]
        private void GetDependencies(IPlayerInput playerInput, IPlayerSettings settings,
            ISpellObjectsFactory spellObjectsFactory, ISpellTypesSetting spellTypesSetting)
        {
            _playerInput = playerInput;
            _settings = settings;
            _spellObjectsFactory = spellObjectsFactory;
            _spellTypesSetting = spellTypesSetting;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_cameraFollowObject, _spellSpawnObject, _wallChecker, _groundChecker};

        protected override void Initialize()
        {
            var playerMovement =
                new PlayerMovement(_thisRigidbody, _settings.Movement, _groundChecker, _wallChecker, this);

            var playerSpellsManager = new PlayerSpellsManager(new List<ISpell>(_startTestSpells),
                _spellSpawnObject.ReadonlyTransform, _playerCaster, _spellObjectsFactory, _spellTypesSetting);
            playerSpellsManager.AddSpell(_spellTypesSetting.LastChanceSpellType,
                _settings.SpellManager.LastChanceSpell);

            var playerLook = new PlayerLook(_camera, _cameraFollowObject.ReadonlyTransform, _objectToRotateHorizontally,
                _settings.Look);

            _itemsNeedDisabling.Add(playerMovement);
            _itemsNeedDisabling.Add(playerSpellsManager);

            var controllerToSetup = GetComponent<IInitializablePlayerController>();
            var setupData = new PlayerControllerSetupData(_eventInvokerForAnimations, _playerCameraEffects,
                _playerVisual, _playerCharacter, playerSpellsManager, _playerInput, playerMovement, playerLook,
                _idHolder, _itemsNeedDisabling, _cameraTransform, _pointForAiming.ReadonlyTransform);
            controllerToSetup.Initialize(setupData);
        }

        protected override void Prepare()
        {
            _playerCaster = GetComponent<ICaster>();
            _idHolder = GetComponent<IdHolder>();
            _thisRigidbody = GetComponent<Rigidbody>();

            var playerCharacter = new PlayerCharacter(this, _settings.Character);

            _itemsNeedDisabling = new List<IDisableable> {playerCharacter};

            _playerCharacter = playerCharacter;

            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator, _settings.Visual);
            _playerCameraEffects = new PlayerCameraEffects(_settings.CameraEffects, _camera, _cameraEffectsGameObject);

            _cameraTransform = new ReadonlyTransform(_camera.transform);
        }
    }
}