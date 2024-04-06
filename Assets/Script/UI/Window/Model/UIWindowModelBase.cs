using Common.Id_Holder;

namespace UI.Window.Model
{
    public abstract class UIWindowModelBase : IUIWindowModel
    {
        private readonly IIdHolder _idHolder;

        protected UIWindowModelBase(IIdHolder idHolder)
        {
            _idHolder = idHolder;
        }

        public abstract bool CanBeClosedByPlayer { get; }
        public int Id => _idHolder.Id;
        public bool IsOpened { get; private set; }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }
        
        public int CompareTo(IIdHolder other)
        {
            return _idHolder.CompareTo(other);
        }

        public virtual void Appear()
        {
            IsOpened = true;
        }

        public virtual void Disappear()
        {
            IsOpened = false;
        }
    }
}