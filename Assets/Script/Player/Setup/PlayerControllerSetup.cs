﻿using System.Collections.Generic;
using System.Linq;
using Common;
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
using Spells.Concrete_Types.Types;
using Spells.Factory;
using Spells.Spell;
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
        private SpellTypesSetting _spellTypesSetting;
        private List<IDisableable> _itemsNeedDisabling;

        [Inject]
        private void Construct(IPlayerInput playerInput, PlayerSettings settings,
            ISpellObjectsFactory spellObjectsFactory, SpellTypesSetting spellTypesSetting)
        {
            _playerInput = playerInput;
            _settings = settings;
            _spellObjectsFactory = spellObjectsFactory;
            _spellTypesSetting = spellTypesSetting;
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

            var playerSpellsManager =
                new PlayerSpellsManager(_startTestSpells.Cast<ISpell>().ToList(), _spellSpawnObject.ReadonlyTransform,
                    playerCaster, _spellObjectsFactory, _spellTypesSetting);
            playerSpellsManager.AddSpell(_spellTypesSetting.LastChanceSpellType, _settings.SpellManager.LastChanceSpell);

            var playerMovement =
                new PlayerMovement(thisRigidbody, _settings.Movement, _groundChecker, _wallChecker, this);
            
            var playerCharacter = new PlayerCharacter(this, _settings.Character);
            
            _itemsNeedDisabling = new List<IDisableable>
            {
                playerMovement,
                playerCharacter,
                playerSpellsManager
            };
            _playerMovement = playerMovement;
            _playerCharacter = playerCharacter;
            _playerSpellsManager = playerSpellsManager;

            _playerLook = new PlayerLook(_camera, _cameraFollowObject.ReadonlyTransform, _objectToRotateHorizontally,
                _settings.Look);
            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator);
            _playerCameraEffects = new PlayerCameraEffects(_settings.CameraEffects, _camera, _cameraEffectsGameObject);
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