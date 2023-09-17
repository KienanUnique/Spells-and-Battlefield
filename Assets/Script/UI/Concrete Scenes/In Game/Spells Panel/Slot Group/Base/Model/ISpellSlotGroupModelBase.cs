using Spells.Implementations_Interfaces.Implementations;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Model
{
    public interface ISpellSlotGroupModelBase
    {
        public bool IsSelected { get; }
        public ISpellType Type { get; }
        public void Select();
        public void Unselect();
    }
}