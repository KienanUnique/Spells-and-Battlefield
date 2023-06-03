using Pickable_Items.Strategies_For_Pickable_Controller;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    public interface IInitializablePickable3DObjectController : IPickableItem
    {
        void Initialize(IStrategyForPickableController strategyForPickableController, bool needFallDown);
    }
}