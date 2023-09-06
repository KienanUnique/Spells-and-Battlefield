using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Pickable_Items.Card_Controls;
using Pickable_Items.Card_Information;
using Pickable_Items.Setup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pickable_Items.Controllers.Concrete_Types.Card
{
    public class PickableCardControllerSetup : PickableItemControllerSetupBase<IInitializablePickableCardController>,
        ICardInformationSettable
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        private ICardInformation _cardInformation;
        private ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter;

        protected override IEnumerable<IInitializable> AdditionalObjectsToWaitBeforeInitialization =>
            new[] {_externalDependenciesInitializationWaiter};

        public void SetCardInformation(ICardInformation cardInformation)
        {
            _cardInformation = cardInformation;
            if (_externalDependenciesInitializationWaiter == null)
            {
                _externalDependenciesInitializationWaiter = new ExternalDependenciesInitializationWaiter(true);
            }
            else
            {
                _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
            }
        }

        protected override void Initialize(IPickableItemControllerBaseSetupData baseSetupData,
            IInitializablePickableCardController controllerToSetup)
        {
            var cardControls = new CardControls(_title, _icon);
            controllerToSetup.Initialize(baseSetupData, cardControls, _cardInformation);
        }

        protected override void Prepare()
        {
            _externalDependenciesInitializationWaiter ??= new ExternalDependenciesInitializationWaiter(false);
            base.Prepare();
        }
    }
}