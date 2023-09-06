using Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;
using UnityEngine;

namespace Spells.Factory
{
    public interface ISpellObjectsFactory
    {
        public void Create(ISpellDataForSpellController spellData, ISpellPrefabProvider spellPrefabProvider,
            ICaster caster, Vector3 spawnPosition, Quaternion spawnRotation);
    }
}