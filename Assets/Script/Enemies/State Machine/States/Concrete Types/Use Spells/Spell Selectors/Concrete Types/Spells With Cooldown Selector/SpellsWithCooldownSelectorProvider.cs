using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Spells_With_Cooldown_Selector
{
    [CreateAssetMenu(fileName = "Spells With Cooldown Selector",
        menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors + "Spells With Cooldown Selector",
        order = 0)]
    public class SpellsWithCooldownSelectorProvider : SpellsSelectorProvider
    {
        [SerializeField] private List<SpellWithCooldown> _spellsToUseInPriorityOrder;
        public override ISpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter)
        {
            return new SpellsWithCooldownSelectorImplementation(coroutineStarter,
                new List<ISpellWithCooldown>(_spellsToUseInPriorityOrder));
        }
    }
}