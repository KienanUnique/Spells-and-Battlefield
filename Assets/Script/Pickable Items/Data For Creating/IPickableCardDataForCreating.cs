using Pickable_Items.Card_Information;

namespace Pickable_Items.Data_For_Creating
{
    public interface IPickableCardDataForCreating : IPickableItemDataForCreating
    {
        public ICardInformation CardInformation { get; }
    }
}