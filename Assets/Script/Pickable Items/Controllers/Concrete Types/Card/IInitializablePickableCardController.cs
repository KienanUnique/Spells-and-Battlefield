using Pickable_Items.Card_Information;
using Pickable_Items.Strategies_For_Pickable_Controller;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public interface IInitializablePickableCardController : IPickableItem
    {
        public void Initialize(IStrategyForPickableController strategyForPickableController, bool needFallDown,
            ICardInformation cardInformation);
    }
}