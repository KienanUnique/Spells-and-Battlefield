using UI.Window.View;

namespace UI.Window.Setup
{
    public abstract class DefaultWindowPresenterSetupBase : WindowPresenterSetupBase
    {
        protected IUIWindowView View { get; private set; }

        protected override void Prepare()
        {
            base.Prepare();
            View = new DefaultUIWindowView(transform, DefaultUIElementViewSettings);
        }
    }
}