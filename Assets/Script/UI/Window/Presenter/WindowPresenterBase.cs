using Common.Id_Holder;
using UI.Element.Presenter;
using UI.Window.Model;

namespace UI.Window.Presenter
{
    public abstract class WindowPresenterBase : UIElementPresenterBase, IUIWindow
    {
        public int Id => Model.Id;
        public bool CanBeClosedByPlayer => Model.CanBeClosedByPlayer;
        protected abstract IUIWindowModel Model { get; }

        public bool Equals(IIdHolder other)
        {
            return Model.Equals(other);
        }
    }
}