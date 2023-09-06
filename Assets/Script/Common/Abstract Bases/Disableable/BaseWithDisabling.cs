namespace Common.Abstract_Bases.Disableable
{
    public abstract class BaseWithDisabling : IDisableable
    {
        private bool _isEnabled;

        public void Enable()
        {
            if (_isEnabled)
            {
                return;
            }

            SubscribeOnEvents();
            _isEnabled = true;
        }

        public void Disable()
        {
            if (!_isEnabled)
            {
                return;
            }

            UnsubscribeFromEvents();
            _isEnabled = false;
        }

        protected abstract void SubscribeOnEvents();
        protected abstract void UnsubscribeFromEvents();
    }
}