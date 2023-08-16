﻿using System;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Factories;
using Pickable_Items.Controllers.Concrete_Types._3D_Object;
using Pickable_Items.Controllers.Concrete_Types.Card;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Setup;
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
            var createdItem = InstantiatePrefab(dataForCreating.PickableItemPrefabProvider,
                position, Quaternion.identity);

            var strategySettable = createdItem.GetComponent<IPickableItemStrategySettable>();
            strategySettable.SetStrategyForPickableController(dataForCreating.StrategyForController, needItemFallDown);

            switch (dataForCreating)
            {
                case IPickableCardDataForCreating cardDataForCreating:
                {
                    var cardInformationSettable = createdItem.GetComponent<ICardInformationSettable>();
                    cardInformationSettable.SetCardInformation(cardDataForCreating.CardInformation);
                    break;
                }
                case IPickable3DObjectDataForCreating object3DDataForCreating:
                {
                    break;
                }
                default:
                    throw new InvalidPickableItemDataForCreatingType();
            }


            return createdItem.GetComponent<IPickableItem>();
        }

        private class InvalidPickableItemDataForCreatingType : Exception
        {
            public InvalidPickableItemDataForCreatingType() : base("Invalid pickable item data for creating type")
            {
            }
        }
    }
}