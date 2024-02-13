using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Id_Holder;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;
using Factions;
using Player.Animator_Status_Checker;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Press_Key_Interactor;
using Player.Spell_Manager;
using Player.Visual;
using Player.Visual.Hook_Trail;
using Systems.Input_Manager.Concrete_Types.In_Game;

namespace Player.Setup
{
    public class PlayerControllerSetupData : IPlayerControllerSetupData
    {
        public PlayerControllerSetupData(IPlayerCameraEffects playerCameraEffects, IPlayerVisual playerVisual,
            IPlayerCharacter playerCharacter, IPlayerSpellsManager playerSpellsManager, IPlayerInput playerInput,
            IPlayerMovement playerMovement, IPlayerLook playerLook, IIdHolder idHolder,
            List<IDisableable> itemsNeedDisabling, IReadonlyTransform cameraTransform, IFaction setFaction,
            IInformationForSummon setInformationForSummon, IToolsForSummon setToolsForSummon,
            IReadonlyTransform setUpperPointForSummonedEnemiesPositionCalculating,
            IPlayerAnimatorStatusChecker setAnimatorStatusChecker, IHookTrailVisual setHookTrailVisual, IPressKeyInteractor setPressKeyInteractor)
        {
            SetPlayerCameraEffects = playerCameraEffects;
            SetPlayerVisual = playerVisual;
            SetPlayerCharacter = playerCharacter;
            SetPlayerSpellsManager = playerSpellsManager;
            SetPlayerInput = playerInput;
            SetPlayerMovement = playerMovement;
            SetPlayerLook = playerLook;
            SetIDHolder = idHolder;
            SetItemsNeedDisabling = itemsNeedDisabling;
            SetCameraTransform = cameraTransform;
            SetFaction = setFaction;
            SetInformationForSummon = setInformationForSummon;
            SetToolsForSummon = setToolsForSummon;
            SetUpperPointForSummonedEnemiesPositionCalculating = setUpperPointForSummonedEnemiesPositionCalculating;
            SetAnimatorStatusChecker = setAnimatorStatusChecker;
            SetHookTrailVisual = setHookTrailVisual;
            SetPressKeyInteractor = setPressKeyInteractor;
        }

        public List<IDisableable> SetItemsNeedDisabling { get; }
        public IIdHolder SetIDHolder { get; }
        public IPlayerLook SetPlayerLook { get; }
        public IPlayerMovement SetPlayerMovement { get; }
        public IPlayerInput SetPlayerInput { get; }
        public IPlayerSpellsManager SetPlayerSpellsManager { get; }
        public IPlayerCharacter SetPlayerCharacter { get; }
        public IPlayerVisual SetPlayerVisual { get; }
        public IPlayerCameraEffects SetPlayerCameraEffects { get; }
        public IReadonlyTransform SetCameraTransform { get; }
        public IFaction SetFaction { get; }
        public IInformationForSummon SetInformationForSummon { get; }
        public IToolsForSummon SetToolsForSummon { get; }
        public IReadonlyTransform SetUpperPointForSummonedEnemiesPositionCalculating { get; }
        public IPlayerAnimatorStatusChecker SetAnimatorStatusChecker { get; }
        public IHookTrailVisual SetHookTrailVisual { get; }
        public IPressKeyInteractor SetPressKeyInteractor { get; }
    }
}