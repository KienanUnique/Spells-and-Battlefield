using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Checkers;
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
using Systems.Input_Manager;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Setup
{
    public interface IPlayerControllerSetupData
    {
        List<IDisableable> SetItemsNeedDisabling { get; }
        IIdHolder SetIDHolder { get; }
        IPlayerLook SetPlayerLook { get; }
        IPlayerMovement SetPlayerMovement { get; }
        IPlayerInput SetPlayerInput { get; }
        IPlayerSpellsManager SetPlayerSpellsManager { get; }
        IPlayerCharacter SetPlayerCharacter { get; }
        IPlayerVisual SetPlayerVisual { get; }
        IPlayerCameraEffects SetPlayerCameraEffects { get; }
        IPlayerEventInvokerForAnimations SetPlayerEventInvokerForAnimations { get; }
    }
}