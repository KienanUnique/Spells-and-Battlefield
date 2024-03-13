using System;
using System.Collections;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers
{
    public abstract class MechanismsTriggerBase : InitializableMonoBehaviourBase, IMechanismsTrigger
    {
        private bool _needTriggerOneTime;
        private bool _isCooldownNow;
        private float _triggerDelayInSeconds;
        public event Action Triggered;
        protected bool WasTriggered { private set; get; }

        protected void InitializeBase(MechanismsTriggerBaseSetupData setupData)
        {
            _needTriggerOneTime = setupData.NeedTriggerOneTime;
            _triggerDelayInSeconds = setupData.TriggerDelayInSeconds;
            SetInitializedStatus();
        }

        protected void TryInvokeTriggerEvent()
        {
            if (_needTriggerOneTime && WasTriggered || _isCooldownNow)
            {
                return;
            }

            if (_triggerDelayInSeconds == 0f)
            {
                Trigger();
            }
            else
            {
                StartCoroutine(TriggerAfterCooldown());
            }
        }

        private IEnumerator TriggerAfterCooldown()
        {
            _isCooldownNow = true;
            yield return new WaitForSeconds(_triggerDelayInSeconds);
            _isCooldownNow = false;
            Trigger();
        }

        private void Trigger()
        {
            WasTriggered = true;
            Triggered?.Invoke();
        }
    }
}