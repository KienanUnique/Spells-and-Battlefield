using Common.Id_Holder;
using UI.Element;
using UI.Window.Model;

namespace UI.Window
{
    public interface IUIWindow : IUIElement, IUICanBeClosedByPlayerStatus, IIdHolder
    {
    }
}