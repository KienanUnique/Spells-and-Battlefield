using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Base.Setup;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.View;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot_Group.Concrete_Types.Last_Chance_Spell_Slot_Group.Presenter
{
    public interface IIInitializableLastChanceSpellSlotGroupPresenter : IInitializableSpellSlotGroupPresenter<
        ILastChanceSpellGroupModel, ILastChanceSpellSlotGroupView>
    {
    }
}