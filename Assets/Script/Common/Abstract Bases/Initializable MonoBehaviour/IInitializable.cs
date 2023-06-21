using System;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public interface IInitializable
    {
        public event Action<InitializationStatus> InitializationStatusChanged;
        public InitializationStatus CurrentInitializationStatus { get; }
    }
}