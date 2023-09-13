using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common
{
    public class ExternalDependenciesInitializationWaiter : IInitializable
    {
        public ExternalDependenciesInitializationWaiter(bool needInstantlyHandleExternalDependenciesInitialization)
        {
            CurrentInitializableMonoBehaviourStatus = InitializableMonoBehaviourStatus.NonInitialized;
            if (needInstantlyHandleExternalDependenciesInitialization)
            {
                HandleExternalDependenciesInitialization();
            }
        }

        public event Action<InitializableMonoBehaviourStatus> InitializationStatusChanged;
        public InitializableMonoBehaviourStatus CurrentInitializableMonoBehaviourStatus { get; private set; }

        public void HandleExternalDependenciesInitialization()
        {
            CurrentInitializableMonoBehaviourStatus = InitializableMonoBehaviourStatus.Initialized;
            InitializationStatusChanged?.Invoke(CurrentInitializableMonoBehaviourStatus);
        }
    }
}