using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Puzzles.Mechanisms_Triggers
{
    public abstract class MechanismsTriggerBase : InitializableMonoBehaviourBase, IMechanismsTrigger
    {
        public abstract event Action Triggered;
    }
}