using Spells.Implementations_Interfaces.Implementations;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group
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