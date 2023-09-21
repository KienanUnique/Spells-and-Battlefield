using System;
using System.Collections;
using System.Collections.ObjectModel;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Collection_With_Reaction_On_Change;
using Common.Event_Invoker_For_Action_Animations;
using Common.Id_Holder;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Factions;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Setup;
using Player.Spell_Manager;
using Player.Visual;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Systems.Input_Manager;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControllerSetup))]
    public class PlayerController : InitializableMonoBehaviourBase,
        IPlayer,
        ICoroutineStarter,
        IInitializablePlayerController
    {
        private IEventInvokerForActionAnimations _eventInvokerForAnimations;
        private IIdHolder _idHolder;
        private IPlayerCameraEffects _cameraEffects;
        private IPlayerCharacter _character;
        private IPlayerInput _input;
        private IPlayerLook _look;
        private IPlayerMovement _movement;
        private IPlayerSpellsManager _spellsManager;
        private IPlayerVisual _visual;

        public void Initialize(IPlayerControllerSetupData setupData)
        {
            _idHolder = setupData.SetIDHolder;
            _look = setupData.SetPlayerLook;
            _movement = setupData.SetPlayerMovement;
            _input = setupData.SetPlayerInput;
            _spellsManager = setupData.SetPlayerSpellsManager;
            _character = setupData.SetPlayerCharacter;
            _visual = setupData.SetPlayerVisual;
            _cameraEffects = setupData.SetPlayerCameraEffects;
            _eventInvokerForAnimations = setupData.SetPlayerEventInvokerForAnimations;
            CameraTransform = setupData.SetCameraTransform;
            UpperPointForSummonedEnemiesPositionCalculating = setupData.SetUpperPointForSummonedEnemiesPositionCalculating;
            Faction = setupData.SetFaction;
            InformationForSummon = setupData.SetInformationForSummon;
            ToolsForSummon = setupData.SetToolsForSummon;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action<CharacterState> CharacterStateChanged;
        public event ICharacterInformationProvider.OnHitPointsCountChanged HitPointsCountChanged;
        public event Action<float> DashCooldownRatioChanged;
        public event Action Dashed;
        public event Action DashAiming;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;

        public IReadonlyTransform MainTransform => MainRigidbody;
        public IReadonlyTransform UpperPointForSummonedEnemiesPositionCalculating { get; private set; }
        public IInformationForSummon InformationForSummon { get; private set; }
        public IToolsForSummon ToolsForSummon { get; private set; }
        public float HitPointCountRatio => _character.HitPointCountRatio;
        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public IFaction Faction { get; private set; }
        public IReadonlyRigidbody MainRigidbody => _movement.MainRigidbody;
        public IReadonlyTransform PointForAiming => UpperPointForSummonedEnemiesPositionCalculating;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _movement.CurrentPosition;
        public float CurrentDashCooldownRatio => _movement.CurrentDashCooldownRatio;
        public IReadonlyTransform CameraTransform { get; private set; }
        public ISpellType SelectedType => _spellsManager.SelectedType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _spellsManager.Spells;

        public void DieInstantly()
        {
            _character.DieInstantly();
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _character.ApplyContinuousEffect(effect);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _character.HandleDamage(countOfHealthPoints);
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void HandleHeal(int countOfHitPoints)
        {
            _character.HandleHeal(countOfHitPoints);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _movement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _movement.DivideSpeedRatioBy(speedRatio);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _movement.AddForce(force, mode);
        }

        public void AddSpell(ISpell newSpell)
        {
            _spellsManager.AddSpell(newSpell);
        }

        public void InteractAsSpellType(ISpellType spellType)
        {
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _movement.StickToPlatform(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _movement.UnstickFromPlatform();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;

            _input.JumpInputted += _movement.TryJumpInputted;
            _input.StartDashAimingInputted += _movement.TryStartDashAiming;
            _input.DashInputted += OnDashInputted;
            _input.UseSpellInputted += OnUseSpellInputted;
            _input.MoveInputted += _movement.MoveInputted;
            _input.LookInputted += _look.LookInputtedWith;
            _input.SelectSpellType += _spellsManager.SelectSpellType;

            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger += OnCastSpellEventInvokerForAnimationMoment;
            _eventInvokerForAnimations.ActionAnimationEnd += _spellsManager.HandleAnimationEnd;

            _movement.GroundJump += _visual.PlayGroundJumpAnimation;
            _movement.Fall += _visual.PlayFallAnimation;
            _movement.Land += _visual.PlayLandAnimation;
            _movement.StartWallRunning += OnStartWallRunning;
            _movement.WallRunningDirectionChanged += OnWallRunningDirectionChanged;
            _movement.EndWallRunning += OnEndWallRunning;
            _movement.DashAiming += OnDashAiming;
            _movement.Dashed += OnDashed;
            _movement.DashCooldownRatioChanged += OnDashCooldownRatioChanged;

            _character.CharacterStateChanged += OnCharacterStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;

            _spellsManager.NeedPlaySpellAnimation += OnNeedPlaySpellAnimation;
            _spellsManager.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellCanNotBeUsed;
            _spellsManager.SelectedSpellTypeChanged += OnSelectedSpellTypeChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;

            _input.JumpInputted -= _movement.TryJumpInputted;
            _input.StartDashAimingInputted -= _movement.TryStartDashAiming;
            _input.DashInputted -= OnDashInputted;
            _input.UseSpellInputted -= OnUseSpellInputted;
            _input.MoveInputted -= _movement.MoveInputted;
            _input.LookInputted -= _look.LookInputtedWith;
            _input.SelectSpellType -= _spellsManager.SelectSpellType;

            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger -= OnCastSpellEventInvokerForAnimationMoment;
            _eventInvokerForAnimations.ActionAnimationEnd -= _spellsManager.HandleAnimationEnd;

            _movement.GroundJump -= _visual.PlayGroundJumpAnimation;
            _movement.Fall -= _visual.PlayFallAnimation;
            _movement.Land -= _visual.PlayLandAnimation;
            _movement.StartWallRunning -= OnStartWallRunning;
            _movement.WallRunningDirectionChanged -= OnWallRunningDirectionChanged;
            _movement.EndWallRunning -= OnEndWallRunning;
            _movement.DashAiming -= OnDashAiming;
            _movement.Dashed -= OnDashed;
            _movement.DashCooldownRatioChanged -= OnDashCooldownRatioChanged;

            _character.CharacterStateChanged -= OnCharacterStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;

            _spellsManager.NeedPlaySpellAnimation -= OnNeedPlaySpellAnimation;
            _spellsManager.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellCanNotBeUsed;
            _spellsManager.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
        }

        private void OnDashCooldownRatioChanged(float newCooldownRatio)
        {
            DashCooldownRatioChanged?.Invoke(newCooldownRatio);
        }

        private void OnInitializationStatusChanged(InitializableMonoBehaviourStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializableMonoBehaviourStatus.Initialized:
                    StartCoroutine(UpdateMovingDataCoroutine());
                    break;
                case InitializableMonoBehaviourStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private IEnumerator UpdateMovingDataCoroutine()
        {
            while (true)
            {
                _visual.UpdateMovingData(_movement.NormalizedVelocityDirectionXY,
                    _movement.RatioOfCurrentVelocityToMaximumVelocity);
                yield return null;
            }
        }

        private void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue,
            TypeOfHitPointsChange typeOfHitPointsChange)
        {
            HitPointsCountChanged?.Invoke(hitPointsLeft, hitPointsChangeValue, typeOfHitPointsChange);
        }

        private void OnDashed()
        {
            Dashed?.Invoke();
            _cameraEffects.PlayIncreaseFieldOfViewAnimation();
        }

        private void OnDashAiming()
        {
            DashAiming?.Invoke();
        }

        private void OnStartWallRunning(WallDirection direction)
        {
            _cameraEffects.Rotate(direction);
            _visual.PlayLandAnimation();
        }

        private void OnWallRunningDirectionChanged(WallDirection direction)
        {
            _cameraEffects.Rotate(direction);
        }

        private void OnEndWallRunning()
        {
            _cameraEffects.ResetRotation();
            _visual.PlayFallAnimation();
        }

        private void OnDashInputted()
        {
            _movement.TryDash(_look.CameraForward);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _movement.DisableMoving();
                _visual.PlayDieAnimation();
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnUseSpellInputted()
        {
            _spellsManager.TryCastSelectedSpell();
        }

        private void OnNeedPlaySpellAnimation(IAnimationData spellAnimationData)
        {
            _visual.PlayUseSpellAnimation(spellAnimationData);
        }

        private void OnCastSpellEventInvokerForAnimationMoment()
        {
            _spellsManager.CreateSelectedSpell(_look.CameraLookPointPosition);
        }

        private void OnSelectedSpellTypeChanged(ISpellType spellType)
        {
            SelectedSpellTypeChanged?.Invoke(spellType);
        }

        private void OnTryingToUseEmptySpellCanNotBeUsed(ISpellType spellType)
        {
            TryingToUseEmptySpellTypeGroup?.Invoke(spellType);
        }
    }
}