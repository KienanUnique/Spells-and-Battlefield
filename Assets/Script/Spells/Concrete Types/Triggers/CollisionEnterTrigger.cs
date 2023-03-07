using UnityEngine;

[CreateAssetMenu(fileName = "Collision Enter Trigger", menuName = "Spells and Battlefield/Spell System/Trigger/Collision Enter Trigger", order = 0)]
public class CollisionEnterTrigger : SpellTriggerScriptableObject
{
    [SerializeField] float _timeBeforeFinishTrigger;
    public override SpellTriggerCheckStatusEnum CheckContact(Collider other) => SpellTriggerCheckStatusEnum.Finish;
    public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) => _timeBeforeFinishTrigger > timePassedFromInitialize ? SpellTriggerCheckStatusEnum.Wait : SpellTriggerCheckStatusEnum.Finish;
}