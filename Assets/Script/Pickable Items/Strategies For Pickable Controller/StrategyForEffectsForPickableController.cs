using Interfaces.Pickers;
using Spells.Implementations_Interfaces.Implementations;

namespace Pickable_Items.Strategies_For_Pickable_Controller
{
    public class StrategyForEffectsForPickableController : IStrategyForPickableController
    {
        private readonly ISpellMechanicEffect _mechanicEffect;

        public StrategyForEffectsForPickableController(ISpellMechanicEffect mechanicEffect)
        {
            _mechanicEffect = mechanicEffect;
        }

        public bool CanBePickedUpByThisPeeker(IPickableItemsPicker picker)
        {
            return picker is IPickableEffectPicker;
        }

        public void HandlePickUp(IPickableItemsPicker picker)
        {
            if (picker is IPickableEffectPicker droppedEffectPicker)
            {
                _mechanicEffect.ApplyEffectToTarget(droppedEffectPicker);
            }
        }
    }
}