using Pickable_Items.Card_Controls;
using Pickable_Items.Card_Information;
using Pickable_Items.Setup;
using UnityEngine;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    [RequireComponent(typeof(PickableCardControllerSetup))]
    public class PickableCardController : PickableItemControllerBase, IInitializablePickableCardController
    {
        public void Initialize(IPickableItemControllerBaseSetupData setupData, ICardControls cardControls,
            ICardInformation cardInformation)
        {
            cardControls.Title.SetText(cardInformation.Title);
            cardControls.Icon.sprite = cardInformation.Icon;
            base.Initialize(setupData);
        }
    }
}