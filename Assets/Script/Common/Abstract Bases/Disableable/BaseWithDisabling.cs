namespace Common.Abstract_Bases.Disableable
{
    public abstract class BaseWithDisabling : IDisableable
    {
        protected bool IsEnabled { get; private set; }

        public virtual void Enable()
        {
            if (IsEnabled)
            {
                return;
            }

            SubscribeOnEvents();
            IsEnabled = true;
        }

        public virtual void Disable()
        {
            if (!IsEnabled)
            {
                return;
            }

            UnsubscribeFromEvents();
            IsEnabled = false;
        }

        protected abstract void SubscribeOnEvents();
        protected abstract void UnsubscribeFromEvents();
    }
}