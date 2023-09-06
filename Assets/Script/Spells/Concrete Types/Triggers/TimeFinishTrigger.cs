using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Time Finish Trigger",
        menuName = ScriptableObjectsMenuDirectories.SpellTriggerDirectory + "Time Finish Trigger", order = 0)]
    public class TimeFinishTrigger : SpellTriggerScriptableObject
    {
        [SerializeField] private float _timeBeforeFinishTrigger;

        public override ISpellTrigger GetImplementationObject()
        {
            return new TimeFinishTriggerImplementation(_timeBeforeFinishTrigger);
        }

        private class TimeFinishTriggerImplementation : SpellTriggerImplementationBase
        {
            private readonly float _timeBeforeFinishTrigger;
            private bool _wasTriggered;

            public TimeFinishTriggerImplementation(float timeBeforeFinishTrigger)
            {
                _timeBeforeFinishTrigger = timeBeforeFinishTrigger;
            }

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
            {
                return SpellTriggerCheckStatusEnum.Ignore;
            }

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
            {
                if (!_wasTriggered && _timeBeforeFinishTrigger < timePassedFromInitialize)
                {
                    _wasTriggered = true;
                    return SpellTriggerCheckStatusEnum.Finish;
                }

                return SpellTriggerCheckStatusEnum.Ignore;
            }
        }
    }
}