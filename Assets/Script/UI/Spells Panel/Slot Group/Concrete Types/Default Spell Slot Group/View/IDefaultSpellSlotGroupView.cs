using UI.Spells_Panel.Slot_Group.Base.View;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View
{
    public interface IDefaultSpellSlotGroupView : ISpellSlotGroupViewBase
    {
        public void UpdateGroupCount(int newCount);
    }
}