using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Readonly_Transform;
using Interfaces;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Spell_Manager;
using Player.Visual;
using Systems.Input_Manager;

namespace Player.Setup
{
    public class PlayerControllerSetupData : IPlayerControllerSetupData
    {
        public PlayerControllerSetupData(IEventInvokerForActionAnimations playerEventInvokerForAnimations,
            IPlayerCameraEffects playerCameraEffects, IPlayerVisual playerVisual, IPlayerCharacter playerCharacter,
            IPlayerSpellsManager playerSpellsManager, IPlayerInput playerInput, IPlayerMovement playerMovement,
            IPlayerLook playerLook, IIdHolder idHolder, List<IDisableable> itemsNeedDisabling,
            IReadonlyTransform cameraTransform, IReadonlyTransform setPointForAiming)
        {
            SetPlayerEventInvokerForAnimations = playerEventInvokerForAnimations;
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
            SetPointForAiming = setPointForAiming;
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
        public IEventInvokerForActionAnimations SetPlayerEventInvokerForAnimations { get; }
        public IReadonlyTransform SetCameraTransform { get; }
        public IReadonlyTransform SetPointForAiming { get; }
    }
}