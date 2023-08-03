using System;
using System.Collections.Generic;
using ModestTree;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Disposable_Spells_Selector
{
    public class DisposableSpellsSelectorImplementation : SpellSelectorBase
    {
        private readonly Queue<ISpell> _spellsToUseInPriorityOrder;
    
        public DisposableSpellsSelectorImplementation(Queue<ISpell> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
        }
    
        public override bool CanUseSpell => !_spellsToUseInPriorityOrder.IsEmpty();
        public override event Action CanUseSpellsAgain;
    
        public override ISpell Pop()
        {
            return _spellsToUseInPriorityOrder.Dequeue();
        }
    }
}