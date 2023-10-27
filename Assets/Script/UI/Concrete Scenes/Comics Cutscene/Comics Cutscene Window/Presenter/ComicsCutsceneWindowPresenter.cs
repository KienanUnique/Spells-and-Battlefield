using Systems.Input_Manager.Concrete_Types.Comics_Cutscene;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Setup;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Presenter
{
    public class ComicsCutsceneWindowPresenter : WindowPresenterBase, IInitializableComicsCutsceneWindowPresenter
    {
        private IComicsCutsceneWindowModel _model;
        private IUIWindowView _view;
        private IComicsCutsceneInputManager _inputManager;

        public void Initialize(IComicsCutsceneWindowModel model, IUIWindowView view,
            IComicsCutsceneInputManager inputManager)
        {
            _model = model;
            _view = view;
            _inputManager = inputManager;
            SetInitializedStatus();
        }

        protected override IUIWindowModel WindowModel => _model;
        protected override IUIWindowView WindowView => _view;

        protected override void SubscribeOnWindowEvents()
        {
            _model.NewScreenOpened += OnNewScreenOpened;
            _inputManager.SkipAnimation += _model.SkipPanelAnimation;
            if (!_model.IsComicsPlaying)
            {
                return;
            }

            SubscribeOnScreenEvents(_model.CurrentScreen);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _model.NewScreenOpened -= OnNewScreenOpened;
            _inputManager.SkipAnimation -= _model.SkipPanelAnimation;
            if (!_model.IsComicsPlaying)
            {
                return;
            }

            UnsubscribeFromScreenEvents(_model.CurrentScreen);
        }

        private void OnNewScreenOpened(IComicsScreen previousScreen, IComicsScreen newScreen)
        {
            if (previousScreen != null)
            {
                UnsubscribeFromScreenEvents(previousScreen);
            }

            if (newScreen != null)
            {
                SubscribeOnScreenEvents(newScreen);
            }
        }

        private void SubscribeOnScreenEvents(IComicsScreen screenForSubscribing)
        {
            screenForSubscribing.AllPanelsShown += _model.OnAllPanelsShownInCurrentScreen;
        }

        private void UnsubscribeFromScreenEvents(IComicsScreen screenForUnsubscribing)
        {
            screenForUnsubscribing.AllPanelsShown -= _model.OnAllPanelsShownInCurrentScreen;
        }
    }
}