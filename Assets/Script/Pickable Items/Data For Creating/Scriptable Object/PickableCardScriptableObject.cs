using Pickable_Items.Card_Information;
using Pickable_Items.Prefab_Provider;
using Pickable_Items.Prefab_Provider.Concrete_Types;
using UnityEngine;

namespace Pickable_Items.Data_For_Creating.Scriptable_Object
{
    public abstract class PickableCardScriptableObjectBase : PickableItemScriptableObjectBase,
        IPickableCardDataForCreating
    {
        [SerializeField] private PickableCardPrefabProvider _prefabProvider;
        [SerializeField] private CardInformation _cardInformation;

        public override IPickableItemPrefabProvider PickableItemPrefabProvider => _prefabProvider;
        public ICardInformation CardInformation => _cardInformation;
    }
}