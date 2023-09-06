using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Immediate Trigger",
        menuName = ScriptableObjectsMenuDirectories.SpellTriggerDirectory + "Immediate Trigger", order = 0)]
    public class ImmediateTrigger : SpellTriggerScriptableObject
    {
        public override ISpellTrigger GetImplementationObject()
        {
            return new ImmediateTriggerImplementation();
        }

        private class ImmediateTriggerImplementation : SpellTriggerImplementationBase
        {
            private bool _wasTriggered;

            private SpellTriggerCheckStatusEnum TriggerStatus
            {
                get
                {
                    if (!_wasTriggered)
                    {
                        _wasTriggered = true;
                        return SpellTriggerCheckStatusEnum.Finish;
                    }

                    return SpellTriggerCheckStatusEnum.Ignore;
                }
            }

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
            {
                return TriggerStatus;
            }

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
            {
                return TriggerStatus;
            }
        }
    }
}