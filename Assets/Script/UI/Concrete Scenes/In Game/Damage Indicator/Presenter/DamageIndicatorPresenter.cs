using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Model;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Setup;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.View;
using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Damage_Indicator.Presenter
{
    public class DamageIndicatorPresenter : UIElementPresenterBase, IInitializableDamageIndicatorPresenter
    {
        private IDamageIndicatorModel _model;
        private IDamageIndicatorView _view;
        private ICharacterInformationProvider _characterInformation;

        public void Initialize(IDamageIndicatorModel model, IDamageIndicatorView view,
            ICharacterInformationProvider characterInformation)
        {
            _model = model;
            _view = view;
            _characterInformation = characterInformation;
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;

        protected override void SubscribeOnEvents()
        {
            _characterInformation.HitPointsCountChanged += OnHitPointsCountChanged;
            _model.NeedIndicateAboutExternalDamage += OnNeedIndicateAboutExternalDamage;
            _model.NeedIndicateAboutLocalDamage += OnNeedIndicateAboutLocalDamage;
        }

        protected override void UnsubscribeFromEvents()
        {
            _characterInformation.HitPointsCountChanged -= OnHitPointsCountChanged;
            _model.NeedIndicateAboutExternalDamage -= OnNeedIndicateAboutExternalDamage;
            _model.NeedIndicateAboutLocalDamage -= OnNeedIndicateAboutLocalDamage;
        }

        private void OnHitPointsCountChanged(IHitPointsCharacterChangeInformation hitPointsCharacterChangeInformation)
        {
            if (hitPointsCharacterChangeInformation.TypeOfHitPointsChange != TypeOfHitPointsChange.Damage)
            {
                return;
            }

            _model.HandleDamageSourceInformation(hitPointsCharacterChangeInformation.SourceInformation);
        }

        private void OnNeedIndicateAboutExternalDamage(float angle)
        {
            _view.IndicateAboutExternalDamage(angle);
        }

        private void OnNeedIndicateAboutLocalDamage()
        {
            _view.IndicateAboutLocalDamage();
        }
    }
}