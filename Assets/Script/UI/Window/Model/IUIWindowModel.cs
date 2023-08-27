using Interfaces;

namespace UI.Window.Model
{
    public interface IUIWindowModel : IIdHolder
    {
        public bool CanBeClosedByPlayer { get; }
    }
}