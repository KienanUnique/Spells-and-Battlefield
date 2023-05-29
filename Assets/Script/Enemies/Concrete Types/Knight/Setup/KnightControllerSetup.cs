﻿using Common.Abstract_Bases.Character;
using Enemies.Attack_Target_Selector;
using Enemies.Concrete_Types.Knight.Character;
using Enemies.Concrete_Types.Knight.Visual;
using Enemies.Setup;
using General_Settings_in_Scriptable_Objects;
using Settings;
using UnityEngine;
using Zenject;

namespace Enemies.Concrete_Types.Knight.Setup
{
    public class KnightControllerSetup : EnemyControllerSetupBase<IInitializableKnightController>
    {
        [SerializeField] private KnightVisual _knightVisual;
        [SerializeField] private AttackTargetSelectorFromZone _swordTargetSelector;
        private KnightSettings _knightSettings;
        private KnightCharacter _knightCharacter;

        [Inject]
        private void Construct(KnightSettings knightSettings)
        {
            _knightSettings = knightSettings;
        }

        protected override IEnemySettings EnemySettings => _knightSettings;
        protected override CharacterBase Character => _knightCharacter;

        protected override void SetupConcreteController(IEnemyBaseSetupData baseSetupData,
            IInitializableKnightController controllerToSetup)
        {
            controllerToSetup.Initialize(baseSetupData,
                _knightVisual,
                _swordTargetSelector,
                _knightCharacter,
                _knightSettings);
        }

        protected override void SpecialAwakeAction()
        {
            _knightCharacter = new KnightCharacter(this, _knightSettings.KnightCharacterSettings);
        }
    }
}