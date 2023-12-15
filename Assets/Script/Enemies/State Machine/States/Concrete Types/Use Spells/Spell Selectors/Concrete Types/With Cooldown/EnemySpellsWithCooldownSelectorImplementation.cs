using System.Collections.Generic;
using System.Linq;
using Spells.Spell;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    public class EnemySpellsWithCooldownSelectorImplementation : EnemySpellSelectorBase
    {
        private readonly List<ISpellWithCooldown> _spellsToUseInPriorityOrder;
        private ISpellWithCooldown _mostPrioritizedReadyToUseSpellSpell;
        private ISpellWithCooldown _rememberedSpell;

        public EnemySpellsWithCooldownSelectorImplementation(List<ISpellWithCooldown> spellsToUseInPriorityOrder)
        {
            _spellsToUseInPriorityOrder = spellsToUseInPriorityOrder;
            SelectMostPrioritizedReadyToUseSpell();
        }

        public override bool CanUseSpell => _mostPrioritizedReadyToUseSpellSpell != null;
        public override ISpell RememberedSpell => _rememberedSpell?.Spell;

        public override bool TryToRememberSelectedSpellInformation()
        {
            if (!CanUseSpell)
            {
                return false;
            }

            _rememberedSpell = _mostPrioritizedReadyToUseSpellSpell;
            return true;
        }

        public override void RemoveRememberedSpell()
        {
            _rememberedSpell?.StartCooldownTimer();
            _rememberedSpell = null;
            SelectMostPrioritizedReadyToUseSpell();
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
            _mostPrioritizedReadyToUseSpellSpell = _spellsToUseInPriorityOrder.FirstOrDefault(spell => spell.CanUse);
            if (!canUseSpellsBefore && CanUseSpell)
            {
                InvokeCanUseSpellsAgain();
            }
        }
    }
}