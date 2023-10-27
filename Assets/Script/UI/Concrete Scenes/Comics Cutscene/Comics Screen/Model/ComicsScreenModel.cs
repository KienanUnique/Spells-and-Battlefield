using System;
using System.Collections.Generic;
using System.Linq;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Model
{
    public class ComicsScreenModel : IComicsScreenModel
    {
        private readonly IReadOnlyList<IComicsPanel> _panelsInOrder;
        private int _currentPanelIndex;

        public ComicsScreenModel(IReadOnlyList<IComicsPanel> panelsInOrder)
        {
            _panelsInOrder = panelsInOrder;
        }

        public event Action AllPanelsShown;
        private IComicsPanel CurrentPanel => _panelsInOrder[_currentPanelIndex];
        private bool IsAllPanelsShown => _currentPanelIndex >= _panelsInOrder.Count;

        public void SkipPanelAnimation()
        {
            if (!IsAllPanelsShown)
            {
                CurrentPanel.SkipAnimation();
            }
        }

        public void Disappear(Action callbackOnAnimationEnd)
        {
            var callbackWasUsed = false;
            foreach (IComicsPanel panel in _panelsInOrder)
            {
                panel.Disappear(() =>
                {
                    if (!callbackWasUsed && _panelsInOrder.All(panelToCheck => !panelToCheck.IsShown))
                    {
                        callbackWasUsed = true;
                        callbackOnAnimationEnd?.Invoke();
                    }
                });
            }

            _currentPanelIndex = 0;
        }

        public void Appear()
        {
            _currentPanelIndex = 0;
            CurrentPanel.Appear(ShowNextPanel);
        }

        public void Disappear()
        {
            foreach (IComicsPanel panel in _panelsInOrder)
            {
                panel.Disappear();
            }

            _currentPanelIndex = 0;
        }

        private void ShowNextPanel()
        {
            _currentPanelIndex++;
            if (IsAllPanelsShown)
            {
                AllPanelsShown?.Invoke();
                return;
            }

            CurrentPanel.Appear(ShowNextPanel);
        }
    }
}