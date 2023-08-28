using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common
{
    public class ExternalDependenciesInitializationWaiter : IInitializable
    {
        public ExternalDependenciesInitializationWaiter(bool needInstantlyHandleExternalDependenciesInitialization)
        {
            CurrentInitializationStatus = InitializationStatus.NonInitialized;
            if (needInstantlyHandleExternalDependenciesInitialization)
            {
                HandleExternalDependenciesInitialization();
            }
        }

        public void HandleExternalDependenciesInitialization()
        {
            CurrentInitializationStatus = InitializationStatus.Initialized;
            InitializationStatusChanged?.Invoke(CurrentInitializationStatus);
        }

        public event Action<InitializationStatus> InitializationStatusChanged;
        public InitializationStatus CurrentInitializationStatus { get; private set; }
    }
}