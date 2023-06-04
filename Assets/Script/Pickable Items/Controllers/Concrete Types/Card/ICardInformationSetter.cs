using Pickable_Items.Card_Information;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public interface ICardInformationSettable
    {
        public void SetCardInformation(ICardInformation cardInformation);
    }
}