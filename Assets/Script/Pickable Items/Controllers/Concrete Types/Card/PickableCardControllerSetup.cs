using Pickable_Items.Card_Controls;
using Pickable_Items.Card_Information;
using Pickable_Items.Setup;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public class PickableCardControllerSetup : PickableItemControllerSetupBase<IInitializablePickableCardController>, ICardInformationSettable
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        private ICardInformation _cardInformation;

        public void SetCardInformation(ICardInformation cardInformation)
        {
            _cardInformation = cardInformation;
            
            _isChildReadyForSetup.Value = true;
        }

        protected override void SetupConcreteController(IPickableItemControllerBaseSetupData baseSetupData,
            IInitializablePickableCardController controllerToSetup)
        {
            var cardControls = new CardControls(_title, _icon);
            controllerToSetup.Initialize(baseSetupData, cardControls, _cardInformation);
        }
    }
}