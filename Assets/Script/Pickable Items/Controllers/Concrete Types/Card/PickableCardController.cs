using Pickable_Items.Card_Information;
using Pickable_Items.Strategies_For_Pickable_Controller;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public class PickableCardController : PickableItemControllerBase, IInitializablePickableCardController
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;

        public void Initialize(IStrategyForPickableController strategyForPickableController, bool needFallDown,
            ICardInformation cardInformation)
        {
            _title.SetText(cardInformation.Title);
            _icon.sprite = cardInformation.Icon;
            base.Initialize(strategyForPickableController, needFallDown);
        }
    }
}