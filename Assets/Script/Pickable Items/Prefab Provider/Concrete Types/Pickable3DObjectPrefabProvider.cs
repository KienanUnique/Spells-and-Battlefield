using Pickable_Items.Controllers;
using Pickable_Items.Controllers.Concrete_Types._3D_Object;
using UnityEngine;

namespace Pickable_Items.Prefab_Provider.Concrete_Types
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PickableItemsProvidersDirectory + "3D Object Prefab Provider",
        fileName = "3D Object Prefab Provider", order = 0)]
    public class Pickable3DObjectPrefabProvider : ScriptableObject, IPickableItemPrefabProvider
    {
        [SerializeField] private Pickable3DObjectController _prefab;
        public GameObject Prefab => _prefab.gameObject;
    }
}