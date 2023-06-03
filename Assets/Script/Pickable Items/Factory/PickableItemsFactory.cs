using System;
using Common.Abstract_Bases;
using Pickable_Items.Controllers.Concrete_Types._3D_Object;
using Pickable_Items.Controllers.Concrete_Types.Card;
using Pickable_Items.Data_For_Creating;
using UnityEngine;
using Zenject;

namespace Pickable_Items.Factory
{
    public class PickableItemsFactory : FactoryWithInstantiatorBase, IPickableItemsFactory
    {
        public PickableItemsFactory(IInstantiator instantiator, Transform parentTransform)
            : base(instantiator, parentTransform)
        {
        }

        public IPickableItem Create(IPickableItemDataForCreating dataForCreating, Vector3 position,
            bool needItemFallDown)
        {
            IPickableItem createdItem;
            switch (dataForCreating)
            {
                case IPickableCardDataForCreating cardDataForCreating:
                {
                    var createdCard = InstantiatePrefabForComponent<IInitializablePickableCardController>(
                        cardDataForCreating.PickableItemPrefabProvider.Prefab, position, Quaternion.identity);
                    createdCard.Initialize(cardDataForCreating.StrategyForController, needItemFallDown,
                        cardDataForCreating.CardInformation);
                    createdItem = createdCard;
                    break;
                }
                case IPickable3DObjectDataForCreating object3DDataForCreating:
                {
                    var created3DObject = InstantiatePrefabForComponent<IInitializablePickable3DObjectController>(
                        object3DDataForCreating.PickableItemPrefabProvider.Prefab, position, Quaternion.identity);
                    created3DObject.Initialize(object3DDataForCreating.StrategyForController, needItemFallDown);
                    createdItem = created3DObject;
                    break;
                }
                default:
                    throw new InvalidPickableItemDataForCreatingType();
            }


            return createdItem;
        }

        private class InvalidPickableItemDataForCreatingType : Exception
        {
            public InvalidPickableItemDataForCreatingType() : base("Invalid pickable item data for creating type")
            {
            }
        }
    }
}