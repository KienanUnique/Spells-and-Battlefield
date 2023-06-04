using Pickable_Items.Card_Controls;
using Pickable_Items.Card_Information;
using Pickable_Items.Setup;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public interface IInitializablePickableCardController : IPickableItem
    {
        public void Initialize(IPickableItemControllerBaseSetupData setupData, ICardControls cardControls,
            ICardInformation cardInformation);
    }
}