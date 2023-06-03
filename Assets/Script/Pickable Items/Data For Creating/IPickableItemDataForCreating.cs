using Pickable_Items.Prefab_Provider;
using Pickable_Items.Strategies_For_Pickable_Controller;

namespace Pickable_Items.Data_For_Creating
{
    public interface IPickableItemDataForCreating
    {
        public IPickableItemPrefabProvider PickableItemPrefabProvider { get; }
        public IStrategyForPickableController StrategyForController { get; }
    }
}