using UnityEngine;

[CreateAssetMenu(fileName = "Time Finish Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Time Finish Trigger", order = 0)]
public class TimeFinishTrigger : SpellTriggerScriptableObject
{
    [SerializeField] float _timeBeforeFinishTrigger;

    public override ISpellTrigger GetImplementationObject() => new TimeFinishTriggerImplementation(_timeBeforeFinishTrigger);

    private class TimeFinishTriggerImplementation : SpellTriggerImplementationBase
    {
        private float _timeBeforeFinishTrigger;

        public TimeFinishTriggerImplementation(float timeBeforeFinishTrigger) => _timeBeforeFinishTrigger = timeBeforeFinishTrigger;

        public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => SpellTriggerCheckStatusEnum.Wait;
        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => _timeBeforeFinishTrigger > timePassedFromInitialize ? SpellTriggerCheckStatusEnum.Wait : SpellTriggerCheckStatusEnum.Finish;
    }
}