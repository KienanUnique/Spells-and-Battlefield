using UnityEngine;

[CreateAssetMenu(fileName = "Immediate Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Immediate Trigger", order = 0)]
public class ImmediateTrigger : SpellTriggerScriptableObject
{
    public override ISpellTrigger GetImplementationObject() => new ImmediateTriggerImplementation();

    private class ImmediateTriggerImplementation : SpellTriggerImplementationBase
    {
        public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => SpellTriggerCheckStatusEnum.Finish;
        public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => SpellTriggerCheckStatusEnum.Finish;
    }
}