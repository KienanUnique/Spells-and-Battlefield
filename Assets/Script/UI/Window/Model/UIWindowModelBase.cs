using Common.Id_Holder;
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

        public abstract bool CanBeClosedByPlayer { get; }
        public int Id => _idHolder.Id;
        public bool IsOpened { get; private set; }

        protected IUIWindowManager Manager { get; }

        public virtual void Appear()
        {
            IsOpened = true;
        }

        public virtual void Disappear()
        {
            IsOpened = false;
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }
    }
}