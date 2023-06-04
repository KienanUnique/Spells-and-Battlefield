using Pickable_Items.Setup;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    public interface IInitializablePickable3DObjectController : IPickableItem
    {
        public void Initialize(IPickableItemControllerBaseSetupData setupData);
    }
}