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
    public interface IPlayerControllerSetupData
    {
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
    }
}