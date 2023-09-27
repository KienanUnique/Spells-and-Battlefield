using Common.Abstract_Bases.Character;
using UI.Concrete_Scenes.In_Game.Bar.Model.Concrete_Types;
using UI.Concrete_Scenes.In_Game.Bar.View;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Concrete_Scenes.In_Game.Bar.Setup.Concrete_Types.Hit_Points
{
    public abstract class HitPointsBarSetupBase : BarSetupBase
    {
        [SerializeField] private Image _foreground;
        [SerializeField] private Image _foregroundBackground;
        private IBarViewWithAdditionalDisplayOfChangesSettings _settings;
        private IBarView _view;

        [Inject]
        private void GetDependencies(IBarViewWithAdditionalDisplayOfChangesSettings settings)
        {
            _settings = settings;
        }

        protected abstract ICharacterInformationProvider CharacterInformation { get; }

        protected override void Prepare()
        {
            base.Prepare();
            _view = new BarViewWithAdditionalDisplayOfChanges(_foreground, _foregroundBackground, _settings);
        }

        protected override void Initialize()
        {
            var model = new HitPointsBarModel(CharacterInformation);
            AddDisableable(model);
            InitializePresenter(model, _view);
        }
    }
}