using System.Collections.Generic;
using Common.Id_Holder;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen;
using UI.Concrete_Scenes.Comics_Cutscene.Level_Statistics_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model
{
    public class ComicsCutsceneWindowModel : UIWindowModelBase, IComicsCutsceneWindowModel
    {
        private readonly IReadOnlyList<IComicsScreen> _screens;
        private readonly ILevelStatisticsWindow _levelStatisticsWindow;
        private readonly IUIWindowManager _windowManager;
        private int _currentScreenIndex;

        public ComicsCutsceneWindowModel(IIdHolder idHolder, IReadOnlyList<IComicsScreen> screens,
            ILevelStatisticsWindow levelStatisticsWindow, IUIWindowManager windowManager) : base(idHolder)
        {
            _screens = screens;
            _levelStatisticsWindow = levelStatisticsWindow;
            _windowManager = windowManager;
        }

        public delegate void OnNewScreenOpened(IComicsScreen previousScreen, IComicsScreen newScreen);

        public event OnNewScreenOpened NewScreenOpened;
        public IComicsScreen CurrentScreen { get; private set; }
        public bool IsComicsPlaying => CurrentScreen != null;

        public override bool CanBeClosedByPlayer => false;

        public void OnAllPanelsShownInCurrentScreen()
        {
            CurrentScreen.Disappear(OnComicsScreenDisappearAnimationEnd);
        }

        public void SkipPanelAnimation()
        {
            CurrentScreen?.SkipPanelAnimation();
        }

        public override void Appear()
        {
            base.Appear();
            OpenScreenWithIndex(_currentScreenIndex);
        }

        public override void Disappear()
        {
            base.Disappear();
            CurrentScreen?.Disappear();
        }

        private void OnComicsScreenDisappearAnimationEnd()
        {
            _currentScreenIndex++;
            if (_currentScreenIndex >= _screens.Count)
            {
                CurrentScreen = null;
                _windowManager.OpenWindow(_levelStatisticsWindow);
            }
            else
            {
                OpenScreenWithIndex(_currentScreenIndex);
            }
        }

        private void OpenScreenWithIndex(int index)
        {
            var previousScreen = CurrentScreen;
            CurrentScreen = _screens[index];
            NewScreenOpened?.Invoke(previousScreen, CurrentScreen);
            CurrentScreen.Appear();
        }
    }
}