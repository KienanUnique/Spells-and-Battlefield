using UI.Element.View;

namespace UI.Window.Setup
{
    public abstract class DefaultWindowPresenterSetupBase : WindowPresenterSetupBase
    {
        protected IUIElementView View { get; private set; }

        protected override void Prepare()
        {
            base.Prepare();
            View = new DefaultUIElementView(transform, DefaultUIElementViewSettings);
        }
    }
}