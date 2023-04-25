using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class InGameManagerUI : MonoBehaviour
    {
        [SerializeField] private List<UIElementController> _gameUI;
        [SerializeField] private List<UIElementController> _deathMenuUI;
        [SerializeField] private List<UIElementController> _loadingScreen;

        private List<IEnumerable<IElementUI>> _allUIElements;

        public void SwitchToGameUI()
        {
            SwitchUI(_gameUI);
        }

        public void SwitchToDeathMenuUI()
        {
            SwitchUI(_deathMenuUI);
        }

        public void SwitchToLoadingScreen()
        {
            SwitchUI(_loadingScreen);
        }

        private void Awake()
        {
            _allUIElements = new List<IEnumerable<IElementUI>>
            {
                _gameUI,
                _deathMenuUI,
                _loadingScreen
            };
        }

        private void SwitchUI(IEnumerable<IElementUI> nextGroupUI)
        {
            foreach (var elementUI in nextGroupUI)
            {
                elementUI.Appear();
            }

            foreach (var elementGroupUI in _allUIElements.Except(
                         new List<IEnumerable<IElementUI>> {nextGroupUI}))
            {
                foreach (var elementUI in elementGroupUI)
                {
                    elementUI.Disappear();
                }
            }
        }
    }
}