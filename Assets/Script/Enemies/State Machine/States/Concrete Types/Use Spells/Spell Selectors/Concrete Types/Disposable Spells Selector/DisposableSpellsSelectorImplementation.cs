﻿using System.Collections.Generic;
using ModestTree;
using Spells.Spell;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.
    Disposable_Spells_Selector
{
    public class DisposableSpellsSelectorImplementation : SpellSelectorBase
    {
        private readonly Queue<ISpell> _spellsToUseInPriorityOrder;

        public DisposableSpellsSelectorImplementation(Queue<ISpell> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
        }

        public override bool CanUseSpell => !_spellsToUseInPriorityOrder.IsEmpty();

        public override ISpell Pop()
        {
            Debug.Log("DisposableSpellsSelectorImplementation Pop");
            return _spellsToUseInPriorityOrder.Dequeue();
        }
    }
}