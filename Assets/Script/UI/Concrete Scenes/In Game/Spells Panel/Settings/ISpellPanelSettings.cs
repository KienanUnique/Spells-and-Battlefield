using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Group;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings.Sections.Slot;

namespace UI.Concrete_Scenes.In_Game.Spells_Panel.Settings
{
    public interface ISpellPanelSettings
    {
        ISpellGroupSection GroupSection { get; }
        ISpellSlotSection SlotSection { get; }
    }
}