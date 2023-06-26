using Spells.Implementations_Interfaces.Implementations;

namespace UI.Spells_Panel.Slot_Group.Base.Model
{
    public interface ISpellSlotGroupModelBase
    {
        public bool IsSelected { get; }
        public ISpellType Type { get; }
        public void Select();
        public void Unselect();
    }
}