namespace UI.Spells_Panel.Slot_Group.View
{
    public interface ISpellSlotGroupView
    {
        void UpdateGroupCount(int newCount);
        void Select();
        void Unselect();
    }
}