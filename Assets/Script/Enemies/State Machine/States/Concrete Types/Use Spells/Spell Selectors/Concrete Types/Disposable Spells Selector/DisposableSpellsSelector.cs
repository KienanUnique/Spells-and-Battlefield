using System.Collections.Generic;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.
    Disposable_Spells_Selector
{
    [CreateAssetMenu(fileName = "Disposable Spells Selector",
        menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors + "Disposable Spells Selector",
        order = 0)]
    public class DisposableSpellsSelector : SpellsSelectorProvider
    {
        [SerializeField] private List<SpellScriptableObject> _spellsToUseInPriorityOrder;

        public override ISpellSelector GetImplementationObject()
        {
            return new DisposableSpellsSelectorImplementation(new Queue<ISpell>(_spellsToUseInPriorityOrder));
        }
    }
}