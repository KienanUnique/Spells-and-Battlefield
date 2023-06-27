using Spells.Implementations_Interfaces.Implementations;

namespace UI.Spells_Panel.Slot_Group
{
    public interface ISpellSlotGroup
    {
        public bool IsSelected { get; }
        public ISpellType Type { get; }
        public void Select();
        public void Unselect();
        public void PlayAnimationOnTryingToUseEmptySpellTypeGroup();
    }
}