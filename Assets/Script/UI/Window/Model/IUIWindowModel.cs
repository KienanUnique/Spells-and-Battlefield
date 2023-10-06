using Common.Id_Holder;
using UI.Element;

namespace UI.Window.Model
{
    public interface IUIWindowModel : IUIElement, IUICanBeClosedByPlayerStatus, IIdHolder
    {
        public bool IsOpened { get; }
    }
}