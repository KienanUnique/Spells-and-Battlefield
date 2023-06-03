using Pickable_Items.Strategies_For_Pickable_Controller;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    public class Pickable3DObjectController : PickableItemControllerBase, IInitializablePickable3DObjectController
    {
        public new void Initialize(IStrategyForPickableController strategyForPickableController, bool needFallDown)
        {
            base.Initialize(strategyForPickableController, needFallDown);
        }
    }
}