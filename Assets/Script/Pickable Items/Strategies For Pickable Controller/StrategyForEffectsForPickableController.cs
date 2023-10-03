using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Source;
using Pickable_Items.Picker_Interfaces;

namespace Pickable_Items.Strategies_For_Pickable_Controller
{
    public class StrategyForEffectsForPickableController : IStrategyForPickableController
    {
        private readonly IMechanicEffect _mechanicEffect;

        public StrategyForEffectsForPickableController(IMechanicEffect mechanicEffect)
        {
            _mechanicEffect = mechanicEffect;
        }

        public bool CanBePickedUpByThisPeeker(IPickableItemsPicker picker)
        {
            return picker is IPickableEffectPicker;
        }

        public void HandlePickUp(IPickableItemsPicker picker)
        {
            var effectSourceInformation = new EffectSourceInformation(EffectSourceType.Local, picker.MainTransform);
            if (picker is IPickableEffectPicker droppedEffectPicker)
            {
                _mechanicEffect.ApplyEffectToTarget(droppedEffectPicker, effectSourceInformation);
            }
        }
    }
}