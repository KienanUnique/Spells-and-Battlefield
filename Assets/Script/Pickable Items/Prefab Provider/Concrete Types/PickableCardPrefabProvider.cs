using Pickable_Items.Controllers.Concrete_Types.Card;
using UnityEngine;

namespace Pickable_Items.Prefab_Provider.Concrete_Types
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PickableItemsProvidersDirectory + "Card Prefab Provider",
        fileName = "Card Prefab Provider", order = 0)]
    public class PickableCardPrefabProvider : ScriptableObject, IPickableItemPrefabProvider
    {
        [SerializeField] private PickableCardController _prefab;
        public GameObject Prefab => _prefab.gameObject;
    }
}