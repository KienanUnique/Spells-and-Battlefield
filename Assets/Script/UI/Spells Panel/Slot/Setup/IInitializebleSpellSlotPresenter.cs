using Settings.UI;
using Settings.UI.Spell_Panel;
using UI.Spells_Panel.Slot.Model;
using UI.Spells_Panel.Slot.View;

namespace UI.Spells_Panel.Slot.Setup
{
    public interface IInitializableSpellSlotPresenter
    {
        public void Initialize(ISpellSlotModel model, ISpellSlotView view, SpellPanelSettings settings);
    }
}