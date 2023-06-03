using Pickable_Items.Prefab_Provider;
using Pickable_Items.Strategies_For_Pickable_Controller;
using UnityEngine;

namespace Pickable_Items.Data_For_Creating.Scriptable_Object
{
    public abstract class PickableItemScriptableObjectBase : ScriptableObject, IPickableItemDataForCreating
    {
        public abstract IPickableItemPrefabProvider PickableItemPrefabProvider { get; }
        public abstract IStrategyForPickableController StrategyForController { get; }
    }
}