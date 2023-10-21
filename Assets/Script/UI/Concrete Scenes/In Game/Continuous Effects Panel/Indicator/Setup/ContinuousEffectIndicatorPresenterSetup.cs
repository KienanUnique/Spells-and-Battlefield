using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings;
using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.View;
using UI.Element.Setup;
using UI.Element.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Setup
{
    public class ContinuousEffectIndicatorPresenterSetup : UIElementPresenterSetup
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RawImage _image;
        private IInitializableContinuousEffectIndicatorPresenter _presenter;
        private IContinuousEffectIndicatorView _view;
        private IContinuousEffectIndicatorSettings _settings;

        [Inject]
        private void GetDependencies(IContinuousEffectIndicatorSettings settings)
        {
            _settings = settings;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableContinuousEffectIndicatorPresenter>();
            _view = new ContinuousEffectIndicatorView(_slider, _image, transform, _settings);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_view);
        }
    }
}