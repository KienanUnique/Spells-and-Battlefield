using Common.Abstract_Bases;
using Settings.UI;
using Zenject;

namespace UI.Element.Setup
{
    public abstract class UIElementPresenterSetup : SetupMonoBehaviourBase
    {
        protected GeneralUIAnimationSettings _generalUIAnimationSettings;
        
        [Inject]
        private void Construct(GeneralUIAnimationSettings generalUIAnimationSettings)
        {
            _generalUIAnimationSettings = generalUIAnimationSettings;
        }
    }
}