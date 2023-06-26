using UI.Spells_Panel.Slot_Group.Base.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.View;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Default_Spell_Slot_Group.Presenter
{
    public interface IIInitializableDefaultSpellGroupPresenter : IInitializableSpellSlotGroupPresenter<
        IDefaultSpellSlotGroupModel, IDefaultSpellSlotGroupView>
    {
    }
}