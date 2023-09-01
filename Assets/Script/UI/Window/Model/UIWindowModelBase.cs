using Interfaces;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Window.Model
{
    public abstract class UIWindowModelBase : IUIWindowModel
    {
        private readonly IIdHolder _idHolder;

        protected UIWindowModelBase(IIdHolder idHolder, IUIWindowManager manager)
        {
            _idHolder = idHolder;
            Manager = manager;
        }

        public int Id => _idHolder.Id;
        public abstract bool CanBeClosedByPlayer { get; }
        protected IUIWindowManager Manager { get; }

        public virtual void Appear()
        {
        }

        public virtual void Disappear()
        {
        }

        public bool Equals(IIdHolder other) => _idHolder.Equals(other);
    }
}