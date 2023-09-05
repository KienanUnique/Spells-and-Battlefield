using System.Collections.Generic;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Spell_Types_Settings
{
    public interface ISpellTypesSetting
    {
        IReadOnlyList<ISpellType> TypesListInOrder { get; }
        ISpellType LastChanceSpellType { get; }
    }
}