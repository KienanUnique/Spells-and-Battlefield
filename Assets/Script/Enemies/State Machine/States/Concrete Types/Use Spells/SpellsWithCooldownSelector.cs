using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using Interfaces;
using Spells.Spell;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    [Serializable]
    public class SpellsWithCooldownSelector : BaseWithDisabling
    {
        [SerializeField] private List<SpellWithCooldown> _spellsToUseInPriorityOrder;

        private SpellWithCooldown _selectedSpell;

        public void Initialize(ICoroutineStarter coroutineStarter)
        {
            _spellsToUseInPriorityOrder.ForEach(spell => spell.SetCoroutineStarter(coroutineStarter));
            SelectMostPrioritizedReadyToUseSpell();
        }

        public bool CanUseSpell => _selectedSpell != default;
        public event Action CanUseSpellsAgain;

        public ISpell GetSelectedSpellAndStartCooldownTimer()
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
            Debug.Log($"===============================");
            Debug.Log($"canUseSpellsBefore {canUseSpellsBefore}");
            Debug.Log($"CanUseSpell {CanUseSpell}");
            Debug.Log($"_selectedSpell {_selectedSpell == null}");
            if (!canUseSpellsBefore && CanUseSpell)
            {
                CanUseSpellsAgain?.Invoke();
            }
        }
    }
}