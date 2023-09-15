using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Readonly_Transform;
using Enemies.Character;
using Enemies.General_Settings;
using Enemies.Look;
using Enemies.Loot_Dropper;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Factions;
using Interfaces;
using UI.Popup_Text.Factory;

namespace Enemies.Setup.Controller_Setup_Data
{
    public struct EnemyControllerSetupData : IEnemyControllerSetupData
    {
        public EnemyControllerSetupData(IEnemyStateMachineAI setStateMachineAI, IEnemyMovement setMovement,
            List<IDisableable> setItemsNeedDisabling, IIdHolder setIdHolder,
            IGeneralEnemySettings setGeneralEnemySettings,
            IPopupHitPointsChangeTextFactory setPopupHitPointsChangeTextFactory,
            IEnemyTargetFromTriggersSelector setTargetFromTriggersSelector, IEnemyLook setLook,
            IEventInvokerForActionAnimations setEventInvokerForAnimations, IEnemyVisual setVisual,
            IEnemyCharacter setCharacter, IReadonlyTransform setPopupTextHitPointsChangeAppearCenterPoint,
            ILootDropper setLootDropper, IFaction setFaction, IReadonlyTransform setPointForAiming,
            IInformationForSummon setInformationForSummon, IToolsForSummon setToolsForSummon)
        {
            SetStateMachineAI = setStateMachineAI;
            SetMovement = setMovement;
            SetItemsNeedDisabling = setItemsNeedDisabling;
            SetIdHolder = setIdHolder;
            SetGeneralEnemySettings = setGeneralEnemySettings;
            SetPopupHitPointsChangeTextFactory = setPopupHitPointsChangeTextFactory;
            SetTargetFromTriggersSelector = setTargetFromTriggersSelector;
            SetLook = setLook;
            SetEventInvokerForAnimations = setEventInvokerForAnimations;
            SetVisual = setVisual;
            SetCharacter = setCharacter;
            SetPopupTextHitPointsChangeAppearCenterPoint = setPopupTextHitPointsChangeAppearCenterPoint;
            SetLootDropper = setLootDropper;
            SetFaction = setFaction;
            SetPointForAiming = setPointForAiming;
            SetInformationForSummon = setInformationForSummon;
            SetToolsForSummon = setToolsForSummon;
        }

        public IEnemyStateMachineAI SetStateMachineAI { get; }
        public IEnemyMovement SetMovement { get; }
        public List<IDisableable> SetItemsNeedDisabling { get; }
        public IIdHolder SetIdHolder { get; }
        public IGeneralEnemySettings SetGeneralEnemySettings { get; }
        public IPopupHitPointsChangeTextFactory SetPopupHitPointsChangeTextFactory { get; }
        public IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
        public IEnemyLook SetLook { get; }
        public IEventInvokerForActionAnimations SetEventInvokerForAnimations { get; }
        public IEnemyVisual SetVisual { get; }
        public IEnemyCharacter SetCharacter { get; }
        public IReadonlyTransform SetPopupTextHitPointsChangeAppearCenterPoint { get; }
        public ILootDropper SetLootDropper { get; }
        public IFaction SetFaction { get; }
        public IReadonlyTransform SetPointForAiming { get; }
        public IInformationForSummon SetInformationForSummon { get; }
        public IToolsForSummon SetToolsForSummon { get; }
    }
}