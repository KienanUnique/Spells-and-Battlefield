using System.Collections;
using Common.Animation_Data;
using Pickable_Items.Data_For_Creating;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public interface ISpell : IPickableCardDataForCreating, IEqualityComparer
    {
        public IAnimationData SpellAnimationData { get; }
        public ISpellDataForSpellController SpellDataForSpellController { get; }
        public ISpellPrefabProvider SpellPrefabProvider { get; }
        public ISpellType SpellType { get; }
    }
}