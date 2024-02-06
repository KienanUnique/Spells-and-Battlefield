using Common.Abstract_Bases;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers
{
    public abstract class MechanismsTriggerSetupBase : SetupMonoBehaviourBase
    {
        [SerializeField] private bool _needTriggerOneTime;
        [SerializeField] private float _triggerDelayInSeconds;

        protected MechanismsTriggerBaseSetupData BaseSetupData =>
            new MechanismsTriggerBaseSetupData(_needTriggerOneTime, _triggerDelayInSeconds);
    }
}