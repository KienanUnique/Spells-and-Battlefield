using System.Collections.Generic;
using ModestTree;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Disposable
{
    public class DisposableEnemySpellsSelectorImplementation : EnemySpellSelectorBase
    {
        private readonly Queue<ISpell> _spellsToUseInPriorityOrder;
        private ISpell _rememberedSpell;

        public DisposableEnemySpellsSelectorImplementation(Queue<ISpell> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
        }

        public override bool CanUseSpell => !_spellsToUseInPriorityOrder.IsEmpty();
        public override ISpell RememberedSpell => _rememberedSpell;

        public override bool TryToRememberSelectedSpellInformation()
        {
            if (!CanUseSpell)
            {
                return false;
            }

            _rememberedSpell = _spellsToUseInPriorityOrder.Peek();
            return true;
        }

        public override void RemoveRememberedSpell()
        {
            _rememberedSpell = null;
            _spellsToUseInPriorityOrder.Dequeue();
        }
    }
}