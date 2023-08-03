using System.Collections.Generic;
using Interfaces;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Disposable_Spells_Selector
{
    [CreateAssetMenu(fileName = "Disposable Spells Selector",
        menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors + "Disposable Spells Selector",
        order = 0)]
    public class DisposableSpellsSelectorProvider : SpellsSelectorProvider
    {
        [SerializeField] private List<SpellScriptableObject> _spellsToUseInPriorityOrder;
        public override ISpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter)
        {
            return new DisposableSpellsSelectorImplementation(coroutineStarter,
                new Queue<ISpell>(_spellsToUseInPriorityOrder));
        }
    }
}