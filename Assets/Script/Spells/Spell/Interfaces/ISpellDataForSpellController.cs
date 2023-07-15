using System.Collections.Generic;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Spell.Interfaces
{
    public interface ISpellDataForSpellController : ISpellImplementation
    {
        List<ISpell> NextSpellsOnFinish { get; }
        ISpellMovement SpellObjectMovement { get; }
        ISpellTrigger SpellMainTrigger { get; }
        ISpellType SpellType { get; }
        List<ISpellApplier> SpellAppliers { get; }
    }
}