using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Checkers;
using Interfaces;
using Settings;
using Spells.Continuous_Effect;
using Spells.Factory;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using Systems.Input_Manager;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IPlayer, ICoroutineStarter
    {
        [Header("Camera")] [SerializeField] private Transform _cameraFollowObject;
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
        [SerializeField] private Transform _spellSpawnObject;

        private PlayerCameraEffects _playerCameraEffects;
        private PlayerVisual _playerVisual;
        private PlayerCharacter _playerCharacter;
        private PlayerSpellsManager _playerSpellsManager;
        private IPlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerLook _playerLook;
        private IdHolder _idHolder;
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

        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public event Action<float> HitPointsCountChanged;
        public event Action Dashed;
        public event Action DashAiming;

        public float HitPointCountRatio => _playerCharacter.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Transform MainTransform => _playerMovement.MainTransform;
        public Vector3 CurrentPosition => _playerMovement.CurrentPosition;
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState => _playerCharacter.CurrentState;


        public void HandleHeal(int countOfHealthPoints)
        {
            _playerCharacter.HandleHeal(countOfHealthPoints);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _playerCharacter.HandleDamage(countOfHealthPoints);
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _playerCharacter.ApplyContinuousEffect(effect);
        }

        public void AddSpell(ISpell newSpell)
        {
            _playerSpellsManager.AddSpell(newSpell);
        }

        public int CompareTo(object obj)
        {
            return _idHolder.CompareTo(obj);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _playerMovement.AddForce(force, mode);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _playerMovement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _playerMovement.DivideSpeedRatioBy(speedRatio);
        }

        private void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            var thisRigidbody = GetComponent<Rigidbody>();

            _itemsNeedDisabling = new List<IDisableable>();

            _playerMovement = new PlayerMovement(thisRigidbody, _settings.Movement, _groundChecker, _wallChecker, this);
            _playerCharacter = new PlayerCharacter(this, _settings.Character);
            _itemsNeedDisabling.Add(_playerMovement);
            _itemsNeedDisabling.Add(_playerCharacter);

            _playerLook = new PlayerLook(_camera, _cameraFollowObject, _objectToRotateHorizontally, _settings.Look);
            _playerVisual = new PlayerVisual(_rigBuilder, _characterAnimator);
            _playerCameraEffects = new PlayerCameraEffects(_settings.CameraEffects, _camera, _cameraEffectsGameObject);
            _playerSpellsManager =
                new PlayerSpellsManager(_startTestSpells, _spellSpawnObject, this, _spellObjectsFactory);
        }

        private void Update()
        {
            _playerVisual.UpdateMovingData(_playerMovement.NormalizedVelocityDirectionXY,
                _playerMovement.RatioOfCurrentVelocityToMaximumVelocity);
        }

        private void OnEnable()
        {
            SubscribeOnEvents();
            _itemsNeedDisabling.ForEach(item => item.Enable());
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
            _itemsNeedDisabling.ForEach(item => item.Disable());
        }

        private void SubscribeOnEvents()
        {
            _playerInput.JumpInputted += _playerMovement.TryJumpInputted;
            _playerInput.StartDashAimingInputted += _playerMovement.TryStartDashAiming;
            _playerInput.DashInputted += OnDashInputted;
            _playerInput.UseSpellInputted += OnUseSpellInputted;
            _playerInput.MoveInputted += _playerMovement.MoveInputted;
            _playerInput.LookInputted += _playerLook.LookInputtedWith;

            _playerEventInvokerForAnimations.CastSpellAnimationMoment += OnCastSpellEventInvokerForAnimationMoment;

            _playerMovement.GroundJump += _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall += _playerVisual.PlayFallAnimation;
            _playerMovement.Land += _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning += OnStartWallRunning;
            _playerMovement.EndWallRunning += OnEndWallRunning;
            _playerMovement.DashAiming += OnDashAiming;
            _playerMovement.Dashed += OnDashed;
            _playerMovement.DashCooldownFinished += OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick += OnDashCooldownTimerTick;

            _playerCharacter.StateChanged += OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged += OnHitPointsCountChanged;
        }

        private void UnsubscribeFromEvents()
        {
            _playerInput.JumpInputted -= _playerMovement.TryJumpInputted;
            _playerInput.StartDashAimingInputted -= _playerMovement.TryStartDashAiming;
            _playerInput.DashInputted -= OnDashInputted;
            _playerInput.UseSpellInputted -= OnUseSpellInputted;
            _playerInput.MoveInputted -= _playerMovement.MoveInputted;
            _playerInput.LookInputted -= _playerLook.LookInputtedWith;

            _playerEventInvokerForAnimations.CastSpellAnimationMoment -= OnCastSpellEventInvokerForAnimationMoment;

            _playerMovement.GroundJump -= _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall -= _playerVisual.PlayFallAnimation;
            _playerMovement.Land -= _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning -= OnStartWallRunning;
            _playerMovement.EndWallRunning -= OnEndWallRunning;
            _playerMovement.DashAiming -= OnDashAiming;
            _playerMovement.Dashed -= OnDashed;
            _playerMovement.DashCooldownFinished -= OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick -= OnDashCooldownTimerTick;

            _playerCharacter.StateChanged -= OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged -= OnHitPointsCountChanged;
        }

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);

        private void OnDashCooldownTimerTick(float cooldownRatio) => DashCooldownTimerTick?.Invoke(cooldownRatio);
        private void OnDashCooldownFinished() => DashCooldownFinished?.Invoke();

        private void OnDashed()
        {
            Dashed?.Invoke();
            _playerCameraEffects.PlayIncreaseFieldOfViewAnimation();
        }

        private void OnDashAiming()
        {
            DashAiming?.Invoke();
        }

        private void OnStartWallRunning(WallDirection direction)
        {
            _playerCameraEffects.Rotate(direction);
            _playerVisual.PlayLandAnimation();
        }

        private void OnEndWallRunning()
        {
            _playerCameraEffects.ResetRotation();
            _playerVisual.PlayFallAnimation();
        }

        private void OnDashInputted()
        {
            _playerMovement.TryDash(_playerLook.CameraForward);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _playerVisual.PlayDieAnimation();
            }
        }

        private void OnUseSpellInputted()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerVisual.PlayUseSpellAnimation(_playerSpellsManager.SelectedSpellAnimationInformation
                    .CastAnimationAnimatorOverrideController);
            }
        }

        private void OnCastSpellEventInvokerForAnimationMoment()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerSpellsManager.UseSelectedSpell(_playerLook.CameraRotation);
            }
        }
    }
}