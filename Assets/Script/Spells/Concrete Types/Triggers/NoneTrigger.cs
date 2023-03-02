using UnityEngine;

[CreateAssetMenu(fileName = "None Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/None Trigger", order = 0)]
public class NoneTrigger : SpellTriggerScriptableObject{
    public override SpellTriggerCheckStatusEnum CheckCollisionEnter(Collision other) => SpellTriggerCheckStatusEnum.Wait;
    public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => SpellTriggerCheckStatusEnum.Wait;
}