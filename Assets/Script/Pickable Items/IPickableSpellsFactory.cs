using Spells;
using Spells.Spell;
using UnityEngine;

namespace Pickable_Items
{
    public interface IPickableSpellsFactory
    {
        IPickableItem Create(ISpell spellToStore, Vector3 position);
    }
}