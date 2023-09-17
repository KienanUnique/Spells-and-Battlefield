using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Model;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.View;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Slot.Setup
{
    public interface IInitializableSpellSlotPresenter
    {
        public void Initialize(ISpellSlotModel model, ISpellSlotView view, ISpellPanelSettings settings);
    }
}