using UnityEngine;

[CreateAssetMenu(fileName = "Immediate Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Immediate Trigger", order = 0)]
public class ImmediateTrigger : SpellTriggerScriptableObject
{
    public override ISpellTriggerable GetImplementationObject() => new ImmediateTriggerImplementation();

    private class ImmediateTriggerImplementation : SpellTriggerImplementationBase
    {
        private bool _wasTriggered = false;
        public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => TriggerStatus;
        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => TriggerStatus;

        private SpellTriggerCheckStatusEnum TriggerStatus
        {
            get
            {
                if (!_wasTriggered)
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
}