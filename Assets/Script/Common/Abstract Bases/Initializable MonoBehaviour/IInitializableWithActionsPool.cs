using System;

namespace Common.Abstract_Bases.Initializable_MonoBehaviour
{
    public interface IInitializableWithActionsPool : IInitializable
    {
        public void AddActionAfterInitializing(Action action);
    }
}