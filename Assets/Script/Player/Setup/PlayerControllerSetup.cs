using System.Collections.Generic;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Checkers;
using Common.Abstract_Bases.Disableable;
using Common.Readonly_Transform_Getter;
using Interfaces;
using Player.Camera_Effects;
using Player.Character;
using Player.Event_Invoker_For_Animations;
using Player.Look;
using Player.Movement;
using Player.Spell_Manager;
using Player.Visual;
using Settings;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
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

        [Header("Animations")] [SerializeField]
        private RigBuilder _rigBuilder;

        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private PlayerEventInvokerForAnimations _playerEventInvokerForAnimations;

        [Header("Checkers")] [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private WallChecker _wallChecker;

        [Header("Spells")] [SerializeField] private List<SpellScriptableObject> _startTestSpells;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObject;

        private IPlayerCameraEffects _playerCameraEffects;
        private IPlayerVisual _playerVisual;
        private IPlayerCharacter _playerCharacter;
        private IPlayerInput _playerInput;
        private IIdHolder _idHolder;
        private PlayerSettings _settings;
        private ISpellObjectsFactory _spellObjectsFactory;
        private SpellTypesSetting _spellTypesSetting;
        private List<IDisableable> _itemsNeedDisabling;
        private ICaster _playerCaster;
        private Rigidbody _thisRigidbody;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerSettings settings,
            ISpellObjectsFactory spellObjectsFactory, SpellTypesSetting spellTypesSetting)
        {
            _playerInput = playerInput;
            _settings = settings;
            _spellObjectsFactory = spellObjectsFactory;
            _spellTypesSetting = spellTypesSetting;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>
            {
                _cameraFollowObject, _spellSpawnObject, _wallChecker, _groundChecker
            };

        protected override void Prepare()
        {
            _playerCaster = GetComponent<ICaster>();
            _idHolder = GetComponent<IdHolder>();
            _thisRigidbody = GetComponent<Rigidbody>();

            var playerCharacter = new PlayerCharacter(this, _settings.Character);

            _itemsNeedDisabling = new List<IDisableable>
            {
                playerCharacter,
            };

            _playerCharacter = playerCharacter;


            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator);
            _playerCameraEffects = new PlayerCameraEffects(_settings.CameraEffects, _camera, _cameraEffectsGameObject);
        }

        protected override void Initialize()
        {
            var playerMovement =
                new PlayerMovement(_thisRigidbody, _settings.Movement, _groundChecker, _wallChecker, this);

            var playerSpellsManager =
                new PlayerSpellsManager(new List<ISpell>(_startTestSpells), _spellSpawnObject.ReadonlyTransform,
                    _playerCaster, _spellObjectsFactory, _spellTypesSetting);
            playerSpellsManager.AddSpell(_spellTypesSetting.LastChanceSpellType,
                _settings.SpellManager.LastChanceSpell);

            var playerLook = new PlayerLook(_camera, _cameraFollowObject.ReadonlyTransform, _objectToRotateHorizontally,
                _settings.Look);

            _itemsNeedDisabling.Add(playerMovement);
            _itemsNeedDisabling.Add(playerSpellsManager);

            var controllerToSetup = GetComponent<IInitializablePlayerController>();
            var setupData = new PlayerControllerSetupData(
                _playerEventInvokerForAnimations,
                _playerCameraEffects,
                _playerVisual,
                _playerCharacter,
                playerSpellsManager,
                _playerInput,
                playerMovement,
                playerLook,
                _idHolder,
                _itemsNeedDisabling
            );
            controllerToSetup.Initialize(setupData);
        }
    }
}