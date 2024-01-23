using System;
using Common.Abstract_Bases.Factories.Object_Pool;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public abstract class
        InitializableMonoBehaviourWithObjectPoolingBase<TDataForActivation> : InitializableMonoBehaviourBase,
            IObjectPoolItem<TDataForActivation>
    {
        public event Action<IObjectPoolItem<TDataForActivation>> Deactivated;
        public bool IsUsed { get; private set; }

        public virtual void Activate(TDataForActivation dataForActivation)
        {
            if (IsUsed)
            {
                throw new InvalidOperationException();
            }

            IsUsed = true;
        }

        protected virtual void Deactivate()
        {
            IsUsed = false;
            Deactivated?.Invoke(this);
        }
    }
}