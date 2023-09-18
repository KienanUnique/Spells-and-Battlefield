using Common.Abstract_Bases;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers
{
    public abstract class MechanismsTriggerSetupBase : SetupMonoBehaviourBase
    {
        [SerializeField] private bool _needTriggerOneTime;
        protected bool NeedTriggerOneTime => _needTriggerOneTime;
    }
}