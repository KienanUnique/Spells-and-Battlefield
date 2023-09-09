using Pickable_Items.Data_For_Creating;
using UnityEngine;

namespace Pickable_Items.Factory
{
    public interface IPickableItemsFactory
    {
        public IPickableItem Create(IPickableItemDataForCreating dataForCreating, Vector3 position,
            Vector3? dropDirection = null);
    }
}