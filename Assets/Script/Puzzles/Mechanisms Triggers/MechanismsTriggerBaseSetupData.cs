namespace Puzzles.Mechanisms_Triggers
{
    public class MechanismsTriggerBaseSetupData
    {
        public MechanismsTriggerBaseSetupData(bool needTriggerOneTime, float triggerDelayInSeconds)
        {
            NeedTriggerOneTime = needTriggerOneTime;
            TriggerDelayInSeconds = triggerDelayInSeconds;
        }

        public bool NeedTriggerOneTime { get; }
        public float TriggerDelayInSeconds { get; }
    }
}