using System.Collections;
using Common.Animation_Data;
using Enemies.Look_Point_Calculator;
using Pickable_Items.Data_For_Creating;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell.Interfaces;

namespace Spells.Spell
{
    public interface ISpell : IPickableCardDataForCreating, IEqualityComparer
    {
        public ISpellType SpellType { get; }
        public ILookPointCalculator LookPointCalculator { get; }
        public void HandleSpell(ISpellHandler handler);
    }
}