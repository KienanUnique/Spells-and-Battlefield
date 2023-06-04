using Pickable_Items.Setup;
using UnityEngine;

namespace Pickable_Items.Controllers.Concrete_Types._3D_Object
{
    [RequireComponent(typeof(Pickable3DObjectControllerSetup))]
    public class Pickable3DObjectController : PickableItemControllerBase, IInitializablePickable3DObjectController
    {
        public new void Initialize(IPickableItemControllerBaseSetupData setupData)
        {
            base.Initialize(setupData);
        }
    }
}