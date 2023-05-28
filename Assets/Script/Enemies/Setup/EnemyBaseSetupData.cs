using System.Collections.Generic;
using Common;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Pickable_Items;
using Settings;
using Spells.Spell;

namespace Enemies.Setup
{
    public struct EnemyBaseSetupData : IEnemyBaseSetupData
    {
        public EnemyBaseSetupData(IEnemyStateMachineAI enemyStateMachineAI, ISpell spellToDrop,
            IEnemyMovement enemyMovement, List<IDisableable> itemsNeedDisabling, IIdHolder idHolder,
            GeneralEnemySettings generalEnemySettings, IPickableSpellsFactory spellsFactory,
            IEnemyTargetFromTriggersSelector targetFromTriggersSelector)
        {
            this.SetEnemyStateMachineAI = enemyStateMachineAI;
            this.SetSpellToDrop = spellToDrop;
            this.SetEnemyMovement = enemyMovement;
            this.SetItemsNeedDisabling = itemsNeedDisabling;
            this.SetIdHolder = idHolder;
            this.SetGeneralEnemySettings = generalEnemySettings;
            this.SetSpellsFactory = spellsFactory;
            this.SetTargetFromTriggersSelector = targetFromTriggersSelector;
        }

        public IEnemyStateMachineAI SetEnemyStateMachineAI { get; }
        public ISpell SetSpellToDrop { get; }
        public IEnemyMovement SetEnemyMovement { get; }
        public List<IDisableable> SetItemsNeedDisabling { get; }
        public IIdHolder SetIdHolder { get; }
        public GeneralEnemySettings SetGeneralEnemySettings { get; }
        public IPickableSpellsFactory SetSpellsFactory { get; }
        public IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
    }
}