using System.Collections.Generic;
using System.Linq;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.
    Spells_With_Cooldown_Selector;
using Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells With Cooldown Selector",
    menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors + "Spells With Cooldown Selector",
    order = 0)]
public class SpellsWithCooldownSelectorProvider : SpellsSelectorProvider
{
    [SerializeField] private List<SpellWithCooldownData> _spellsToUseInPriorityOrder;

    public override ISpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter)
    {
        var listWithSpells = _spellsToUseInPriorityOrder
            .Select(spellWithCooldownData => new SpellWithCooldown(spellWithCooldownData, coroutineStarter))
            .Cast<ISpellWithCooldown>().ToList();

        return new SpellsWithCooldownSelectorImplementation(listWithSpells);
    }
}