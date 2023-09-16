using Pickable_Items.Picker_Interfaces;

namespace Pickable_Items.Strategies_For_Pickable_Controller
{
    public interface IStrategyForPickableController
    {
        public bool CanBePickedUpByThisPeeker(IPickableItemsPicker picker);
        public void HandlePickUp(IPickableItemsPicker picker);
    }
}