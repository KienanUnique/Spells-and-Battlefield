using UnityEngine;

[CreateAssetMenu(fileName = "Collision Enter Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Collision Enter Trigger", order = 0)]
public class CollisionEnterTrigger : SpellTriggerScriptableObject
{
    [SerializeField] private float _timeBeforeFinishTrigger;

    public override ISpellTriggerable GetImplementationObject() => new CollisionEnterTriggerImplementation(_timeBeforeFinishTrigger);

    private class CollisionEnterTriggerImplementation : SpellTriggerImplementationBase
    {
        private float _timeBeforeFinishTrigger;

        public CollisionEnterTriggerImplementation(float timeBeforeFinishTrigger) => _timeBeforeFinishTrigger = timeBeforeFinishTrigger;

        public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => SpellTriggerCheckStatusEnum.Finish;
        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => _timeBeforeFinishTrigger > timePassedFromInitialize ? SpellTriggerCheckStatusEnum.Ignore : SpellTriggerCheckStatusEnum.Finish;
    }
}