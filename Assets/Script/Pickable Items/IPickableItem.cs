using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace Pickable_Items
{
    public interface IPickableItem : IInitializableWithActionsPool
    {
        public void DropItemTowardsDirection(Vector3 direction);
    }
}