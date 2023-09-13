using System;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public interface IInitializable
    {
        public event Action<InitializableMonoBehaviourStatus> InitializationStatusChanged;
        public InitializableMonoBehaviourStatus CurrentInitializableMonoBehaviourStatus { get; }
    }
}