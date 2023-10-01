using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;

namespace UI.Concrete_Scenes.In_Game.Bar.Model.Concrete_Types
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

        private void OnHitPointsCountChanged(IHitPointsCharacterChangeInformation changeInformation)
        {
            UpdateFillAmount();
        }
    }
}