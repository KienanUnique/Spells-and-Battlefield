using System;
using System.Collections.ObjectModel;
using Common.Collection_With_Reaction_On_Change;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManagerInformation
    {
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public ISpellType SelectedSpellType { get; }
        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells { get; }
    }
}