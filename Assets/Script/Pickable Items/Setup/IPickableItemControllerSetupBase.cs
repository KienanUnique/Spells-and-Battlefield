using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Setup
{
    public interface IPickableItemControllerSetupBase
    {
        public void SetBaseSetupData(IStrategyForPickableController strategyForPickableController);

        public void SetBaseSetupData(IStrategyForPickableController strategyForPickableController,
            Vector3 dropDirection);
    }
}