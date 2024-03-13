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
        public IReadonlyTransform SetCameraTransform { get; }
        public IInformationForSummon SetInformationForSummon { get; }
        public IToolsForSummon SetToolsForSummon { get; }
        public IReadonlyTransform SetUpperPointForSummonedEnemiesPositionCalculating { get; }
        public IPlayerAnimatorStatusChecker SetAnimatorStatusChecker { get; }
        public IHookTrailVisual SetHookTrailVisual { get; }
        IPressKeyInteractor SetPressKeyInteractor { get; }
    }
}