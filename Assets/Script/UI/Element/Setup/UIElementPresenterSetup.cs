using Common.Abstract_Bases;
using UI.Element.View.Settings;
using Zenject;

namespace UI.Element.Setup
{
    public abstract class UIElementPresenterSetup : SetupMonoBehaviourBase
    {
        protected IDefaultUIElementViewSettings DefaultUIElementViewSettings;
        
        [Inject]
        private void Construct(IDefaultUIElementViewSettings defaultUIElementViewSettings)
        {
            DefaultUIElementViewSettings = defaultUIElementViewSettings;
        }
    }
}