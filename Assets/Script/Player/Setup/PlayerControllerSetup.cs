using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Checkers;
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
using Spells.Spell.Scriptable_Objects;
using Systems.Input_Manager;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Player.Setup
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerControllerSetup : MonoBehaviour, ICoroutineStarter
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
        private IPlayerSpellsManager _playerSpellsManager;
        private IPlayerInput _playerInput;
        private IPlayerMovement _playerMovement;
        private IPlayerLook _playerLook;
        private IIdHolder _idHolder;
        private PlayerSettings _settings;
        private ISpellObjectsFactory _spellObjectsFactory;
        private List<IDisableable> _itemsNeedDisabling;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerSettings settings,
            ISpellObjectsFactory spellObjectsFactory)
        {
            _playerInput = playerInput;
            _settings = settings;
            _spellObjectsFactory = spellObjectsFactory;
        }

        private void Start()
        {
            PrepareSetupData();
            Setup();
        }

        private void PrepareSetupData()
        {
            var playerCaster = GetComponent<ICaster>();
            _idHolder = GetComponent<IdHolder>();
            var thisRigidbody = GetComponent<Rigidbody>();

            _itemsNeedDisabling = new List<IDisableable>();

            var playerMovement =
                new PlayerMovement(thisRigidbody, _settings.Movement, _groundChecker, _wallChecker, this);
            var playerCharacter = new PlayerCharacter(this, _settings.Character);
            _itemsNeedDisabling.Add(playerMovement);
            _itemsNeedDisabling.Add(playerCharacter);
            _playerMovement = playerMovement;
            _playerCharacter = playerCharacter;

            _playerLook = new PlayerLook(_camera, _cameraFollowObject.ReadonlyTransform, _objectToRotateHorizontally,
                _settings.Look);
            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator);
            _playerCameraEffects = new PlayerCameraEffects(_settings.CameraEffects, _camera, _cameraEffectsGameObject);
            _playerSpellsManager =
                new PlayerSpellsManager(_startTestSpells, _spellSpawnObject.ReadonlyTransform, playerCaster,
                    _spellObjectsFactory);
        }

        private void Setup()
        {
            var controllerToSetup = GetComponent<IInitializablePlayerController>();
            var setupData = new PlayerControllerSetupData(
                _playerEventInvokerForAnimations,
                _playerCameraEffects,
                _playerVisual,
                _playerCharacter,
                _playerSpellsManager,
                _playerInput,
                _playerMovement,
                _playerLook,
                _idHolder,
                _itemsNeedDisabling
            );
            controllerToSetup.Initialize(setupData);
        }
    }
}