using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Immediate Trigger",
        menuName = "Spells and Battlefield/Spell System/Trigger/Immediate Trigger", order = 0)]
    public class ImmediateTrigger : SpellTriggerScriptableObject
    {
        public override ISpellTrigger GetImplementationObject() => new ImmediateTriggerImplementation();

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
}