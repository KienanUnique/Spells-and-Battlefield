using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common.Abstract_Bases.Factories.Object_Pool
{
    public interface IObjectPoolItem<in TDataForActivation> : IInitializable
    {
        public event Action<IObjectPoolItem<TDataForActivation>> Deactivated;
        public bool IsUsed { get; }
        public void Activate(TDataForActivation dataForActivation);
    }
}