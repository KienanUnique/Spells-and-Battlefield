using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.With_Cooldown
{
    [CreateAssetMenu(fileName = "Spells With Cooldown Selector",
        menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors + "Spells With Cooldown Selector",
        order = 0)]
    public class SpellsWithCooldownSelectorProvider : SpellsSelectorProvider
    {
        [SerializeField] private List<SpellWithCooldownData> _spellsToUseInPriorityOrder;

        public override ISpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter)
        {
            List<ISpellWithCooldown> listWithSpells = _spellsToUseInPriorityOrder
                                                      .Select(spellWithCooldownData =>
                                                          new SpellWithCooldown(spellWithCooldownData,
                                                              coroutineStarter))
                                                      .Cast<ISpellWithCooldown>()
                                                      .ToList();

            return new SpellsWithCooldownSelectorImplementation(listWithSpells);
        }
    }
}