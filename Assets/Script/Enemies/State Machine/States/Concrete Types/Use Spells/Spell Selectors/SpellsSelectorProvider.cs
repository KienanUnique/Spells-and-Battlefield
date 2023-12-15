using Common.Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors
{
    public abstract class SpellsSelectorProvider : ScriptableObject
    {
        public abstract IEnemySpellSelector GetImplementationObject(ICoroutineStarter coroutineStarter);
    }
}