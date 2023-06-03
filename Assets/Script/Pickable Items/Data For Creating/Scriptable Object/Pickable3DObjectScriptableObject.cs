using Pickable_Items.Prefab_Provider;
using Pickable_Items.Prefab_Provider.Concrete_Types;
using UnityEngine;

namespace Pickable_Items.Data_For_Creating.Scriptable_Object
{
    public abstract class Pickable3DObjectScriptableObject : PickableItemScriptableObjectBase,
        IPickable3DObjectDataForCreating
    {
        [SerializeField] private Pickable3DObjectPrefabProvider _prefabProvider;
        public override IPickableItemPrefabProvider PickableItemPrefabProvider => _prefabProvider;
    }
}