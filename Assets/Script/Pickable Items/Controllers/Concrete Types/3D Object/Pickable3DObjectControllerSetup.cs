using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Pickable_Items.Setup;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    public class Pickable3DObjectControllerSetup
        : PickableItemControllerSetupBase<IInitializablePickable3DObjectController>
    {
        protected override IEnumerable<IInitializable> AdditionalObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize(IPickableItemControllerBaseSetupData baseSetupData,
            IInitializablePickable3DObjectController controllerToSetup)
        {
            controllerToSetup.Initialize(baseSetupData);
        }
    }
}