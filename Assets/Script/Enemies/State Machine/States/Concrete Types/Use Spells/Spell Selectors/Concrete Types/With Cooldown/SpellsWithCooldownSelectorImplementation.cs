using System.Collections.Generic;
using System.Linq;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    public class SpellsWithCooldownSelectorImplementation : SpellSelectorBase
    {
        private readonly List<ISpellWithCooldown> _spellsToUseInPriorityOrder;
        private ISpellWithCooldown _selectedSpell;

        public SpellsWithCooldownSelectorImplementation(List<ISpellWithCooldown> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
            SelectMostPrioritizedReadyToUseSpell();
        }

        public override bool CanUseSpell => _selectedSpell != default;

        public override ISpell Pop()
        {
            ISpell oldSelectedSpell = _selectedSpell.GetSpellAndStartCooldownTimer();
            SelectMostPrioritizedReadyToUseSpell();
            return oldSelectedSpell;
        }

        public override void Enable()
        {
            base.Enable();
            SelectMostPrioritizedReadyToUseSpell();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (ISpellWithCooldown spell in _spellsToUseInPriorityOrder)
            {
                spell.CanUseAgain += SelectMostPrioritizedReadyToUseSpell;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (ISpellWithCooldown spell in _spellsToUseInPriorityOrder)
            {
                spell.CanUseAgain -= SelectMostPrioritizedReadyToUseSpell;
            }
        }

        private void SelectMostPrioritizedReadyToUseSpell()
        {
            bool canUseSpellsBefore = CanUseSpell;
            _selectedSpell = _spellsToUseInPriorityOrder.FirstOrDefault(spell => spell.CanUse);
            if (!canUseSpellsBefore && CanUseSpell)
            {
                InvokeCanUseSpellsAgain();
            }
        }
    }
}