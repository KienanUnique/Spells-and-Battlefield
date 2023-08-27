using Interfaces;

namespace UI.Window.Model
{
    public abstract class UIWindowModelBase : IUIWindowModel
    {
        private readonly IIdHolder _idHolder;

        protected UIWindowModelBase(IIdHolder idHolder)
        {
            _idHolder = idHolder;
        }

        public int Id => _idHolder.Id;
        public abstract bool CanBeClosedByPlayer { get; }

        public virtual void Appear()
        {
        }

        public virtual void Disappear()
        {
        }

        public bool Equals(IIdHolder other) => _idHolder.Equals(other);
    }
}