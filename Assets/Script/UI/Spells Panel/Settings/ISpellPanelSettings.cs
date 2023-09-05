using UI.Spells_Panel.Settings.Sections.Group;
using UI.Spells_Panel.Settings.Sections.Slot;

namespace UI.Spells_Panel.Settings
{
    public interface ISpellPanelSettings
    {
        ISpellGroupSection GroupSection { get; }
        ISpellSlotSection SlotSection { get; }
    }
}