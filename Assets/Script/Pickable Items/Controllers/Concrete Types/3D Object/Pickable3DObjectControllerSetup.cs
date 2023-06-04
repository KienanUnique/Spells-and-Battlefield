using Pickable_Items.Setup;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    public class Pickable3DObjectControllerSetup
        : PickableItemControllerSetupBase<IInitializablePickable3DObjectController>
    {
        private void Start()
        {
            _isChildReadyForSetup.Value = true;
        }

        protected override void SetupConcreteController(IPickableItemControllerBaseSetupData baseSetupData,
            IInitializablePickable3DObjectController controllerToSetup)
        {
            controllerToSetup.Initialize(baseSetupData);
        }
    }
}