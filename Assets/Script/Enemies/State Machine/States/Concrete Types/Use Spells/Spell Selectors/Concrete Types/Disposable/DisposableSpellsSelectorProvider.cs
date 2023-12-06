using System.Collections.Generic;
using Common.Interfaces;
using Spells.Spell;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors.Concrete_Types.Disposable
{
    [CreateAssetMenu(fileName = "Disposable Spells Selector Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemyUseSpellStateSpellsSelectors +
                   "Disposable Spells Selector Provider", order = 0)]
    public class DisposableSpellsSelectorProvider : SpellsSelectorProvider
    {
        [SerializeField] private List<SpellScriptableObjectBase> _spellsToUseInPriorityOrder;

        public override IEnemySpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter)
        {
            return new DisposableEnemySpellsSelectorImplementation(new Queue<ISpell>(_spellsToUseInPriorityOrder));
        }
    }
}