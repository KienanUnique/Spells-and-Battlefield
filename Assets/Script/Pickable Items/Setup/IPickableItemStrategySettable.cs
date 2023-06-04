using Pickable_Items.Strategies_For_Pickable_Controller;

namespace Pickable_Items.Setup
{
    public interface IPickableItemStrategySettable
    {
        public void SetStrategyForPickableController(IStrategyForPickableController strategyForPickableController,
            bool needFallDown);
    }
}