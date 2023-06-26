using UI.Spells_Panel.Slot_Group.Base.Setup;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model;
using UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View;

namespace UI.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Presenter
{
    public interface IIInitializableLastChanceSpellSlotGroupPresenter : IInitializableSpellSlotGroupPresenter<
        ILastChanceSpellGroupModel, ILastChanceSpellSlotGroupView>
    {
    }
}