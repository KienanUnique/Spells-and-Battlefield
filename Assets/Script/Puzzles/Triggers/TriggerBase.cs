using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Puzzles.Triggers
{
    public abstract class TriggerBase : InitializableMonoBehaviourBase, ITrigger
    {
        public abstract event Action Triggered;
    }
}