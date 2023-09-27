using Common.Id_Holder;
using UI.Element.Presenter;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.View;

namespace UI.Window.Presenter
{
    public abstract class WindowPresenterBase : UIElementPresenterBase, IUIWindow
    {
        public int Id => WindowModel.Id;
        public bool CanBeClosedByPlayer => WindowModel.CanBeClosedByPlayer;
        protected abstract IUIWindowModel WindowModel { get; }
        protected abstract IUIWindowView WindowView { get; }
        protected sealed override IUIElementView View => WindowView;

        public bool Equals(IIdHolder other)
        {
            return WindowModel.Equals(other);
        }
    }
}