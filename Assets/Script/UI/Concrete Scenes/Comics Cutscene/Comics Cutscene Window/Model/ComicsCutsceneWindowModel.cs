using System;
using System.Collections.Generic;
using Common.Id_Holder;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model
{
    public class ComicsCutsceneWindowModel : UIWindowModelBase, IComicsCutsceneWindowModel
    {
        private readonly IReadOnlyList<IComicsScreen> _screens;
        private int _currentScreenIndex;

        public ComicsCutsceneWindowModel(IIdHolder idHolder, IReadOnlyList<IComicsScreen> screens) : base(idHolder)
        {
            _screens = screens;
        }

        public delegate void OnNewScreenOpened(IComicsScreen previousScreen, IComicsScreen newScreen);

        public event OnNewScreenOpened NewScreenOpened;
        public event Action ComicsFinished;
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
                ComicsFinished?.Invoke();
            }
            else
            {
                OpenScreenWithIndex(_currentScreenIndex);
            }
        }

        private void OpenScreenWithIndex(int index)
        {
            IComicsScreen previousScreen = CurrentScreen;
            CurrentScreen = _screens[index];
            NewScreenOpened?.Invoke(previousScreen, CurrentScreen);
            CurrentScreen.Appear();
        }
    }
}