using Interfaces;

namespace UI.Bar.Model.Concrete_Types
{
    public class PlayerDashBarModel : BarModelBase
    {
        private readonly IPlayerInformationProvider _playerInformationProvider;
    
        public PlayerDashBarModel(IPlayerInformationProvider playerInformationProvider)
        {
            _playerInformationProvider = playerInformationProvider;
            UpdateFillAmount();
        }
    
        public override float CurrentFillAmount => _playerInformationProvider.CurrentDashCooldownRatio;
    
        protected override void SubscribeOnEvents()
        {
            _playerInformationProvider.DashCooldownRatioChanged += OnDashCooldownRatioTick;
        }
    
        protected override void UnsubscribeFromEvents()
        {
            _playerInformationProvider.DashCooldownRatioChanged -= OnDashCooldownRatioTick;
        }
    
        private void OnDashCooldownRatioTick(float newFillRatio)
        {
            UpdateFillAmount();
        }
    }
}