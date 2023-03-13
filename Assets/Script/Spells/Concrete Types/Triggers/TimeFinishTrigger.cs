using UnityEngine;

[CreateAssetMenu(fileName = "Time Finish Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Time Finish Trigger", order = 0)]
public class TimeFinishTrigger : SpellTriggerScriptableObject
{
    [SerializeField] float _timeBeforeFinishTrigger;

    public override ISpellTriggerable GetImplementationObject() => new TimeFinishTriggerImplementation(_timeBeforeFinishTrigger);

    private class TimeFinishTriggerImplementation : SpellTriggerImplementationBase
    {
        private float _timeBeforeFinishTrigger;
        private bool _wasTriggered = false;
        public TimeFinishTriggerImplementation(float timeBeforeFinishTrigger) => _timeBeforeFinishTrigger = timeBeforeFinishTrigger;

        public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => SpellTriggerCheckStatusEnum.Ignore;
        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
        {
            if (!_wasTriggered && _timeBeforeFinishTrigger < timePassedFromInitialize)
            {
                _wasTriggered = true;
                return SpellTriggerCheckStatusEnum.Finish;
            }
            else
            {
                return SpellTriggerCheckStatusEnum.Ignore;
            }
        }
    }
}