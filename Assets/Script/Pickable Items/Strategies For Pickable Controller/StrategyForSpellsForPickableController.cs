using Interfaces.Pickers;
using Spells.Spell;

namespace Pickable_Items.Strategies_For_Pickable_Controller
{
    public class StrategyForSpellsForPickableController : IStrategyForPickableController
    {
        private readonly ISpell _spell;

        public StrategyForSpellsForPickableController(ISpell spell)
        {
            _spell = spell;
        }

        public bool CanBePickedUpByThisPeeker(IPickableItemsPicker picker)
        {
            return picker is IPickableSpellPicker;
        }

        public void HandlePickUp(IPickableItemsPicker picker)
        {
            (picker as IPickableSpellPicker)?.AddSpell(_spell);
        }
    }
}