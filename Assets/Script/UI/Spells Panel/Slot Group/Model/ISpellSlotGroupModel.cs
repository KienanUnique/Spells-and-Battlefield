using System;
using Spells.Implementations_Interfaces.Implementations;

namespace UI.Spells_Panel.Slot_Group.Model
{
    public interface ISpellSlotGroupModel
    {
        event Action<int> SpellsCountChanged;
        bool IsSelected { get; }
        ISpellType Type { get; }
        void Select();
        void Unselect();
    }
}