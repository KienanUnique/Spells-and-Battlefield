using Common;
using Interfaces;

namespace UI.Bar.Model.Concrete_Types
{
    public class HitPointsBarModel : BarModelBase
    {
        private readonly ICharacterInformationProvider _characterInformation;

        public HitPointsBarModel(ICharacterInformationProvider characterInformation)
        {
            _characterInformation = characterInformation;
            UpdateFillAmount();
        }

        public override float CurrentFillAmount => _characterInformation.HitPointCountRatio;

        protected override void SubscribeOnEvents()
        {
            _characterInformation.HitPointsCountChanged += OnHitPointsCountChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _characterInformation.HitPointsCountChanged -= OnHitPointsCountChanged;
        }

        private void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue,
            TypeOfHitPointsChange typeOfHitPointsChange)
        {
            UpdateFillAmount();
        }
    }
}