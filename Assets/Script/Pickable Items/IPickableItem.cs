using UnityEngine;

namespace Pickable_Items
{
    public interface IPickableItem
    {
        void DropItemTowardsDirection(Vector3 direction);
    }
}