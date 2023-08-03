using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.
    Spells_With_Cooldown_Selector
{
    public class SpellsWithCooldownSelectorImplementation : SpellSelectorBase
    {
        private readonly List<ISpellWithCooldown> _spellsToUseInPriorityOrder;
        private ISpellWithCooldown _selectedSpell;

        public SpellsWithCooldownSelectorImplementation(List<ISpellWithCooldown> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
        }

        public override void Initialize(ICoroutineStarter coroutineStarter)
        {
            base.Initialize(coroutineStarter);
            _spellsToUseInPriorityOrder.ForEach(spell => spell.SetCoroutineStarter(coroutineStarter));
            SelectMostPrioritizedReadyToUseSpell();
        }

        public override bool CanUseSpell => _selectedSpell != default;
        public override event Action CanUseSpellsAgain;

        public override ISpell Pop()
        {
            var oldSelectedSpell = _selectedSpell.GetSpellAndStartCooldownTimer();
            SelectMostPrioritizedReadyToUseSpell();
            return oldSelectedSpell;
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var spell in _spellsToUseInPriorityOrder)
            {
                spell.CanUseAgain += SelectMostPrioritizedReadyToUseSpell;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var spell in _spellsToUseInPriorityOrder)
            {
                spell.CanUseAgain -= SelectMostPrioritizedReadyToUseSpell;
            }
        }

        private void SelectMostPrioritizedReadyToUseSpell()
        {
            var canUseSpellsBefore = CanUseSpell;
            _selectedSpell = _spellsToUseInPriorityOrder.FirstOrDefault(spell => spell.CanUse);
            if (!canUseSpellsBefore && CanUseSpell)
            {
                CanUseSpellsAgain?.Invoke();
            }
        }
    }
}