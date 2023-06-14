using System;
using System.Collections.ObjectModel;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;

namespace Player.Spell_Manager
{
    public interface IPlayerSpellsManagerInformation
    {
        public event Action<ISpellType> SpellUsed;
        public event Action<ISpellType> SpellTypeSlotsIsEmpty;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<ISpellType> NewSpellAdded;
        public ISpellType SelectedType { get; }
        public ReadOnlyDictionary<ISpellType, ReadOnlyCollection<ISpell>> Spells { get; }
    }
}