using Spells.Abstract_Types.Implementation_Bases;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Time Finish Trigger",
        menuName = "Spells and Battlefield/Spell System/Trigger/Time Finish Trigger", order = 0)]
    public class TimeFinishTrigger : SpellTriggerScriptableObject
    {
        [SerializeField] private float _timeBeforeFinishTrigger;

        public override ISpellTrigger GetImplementationObject() =>
            new TimeFinishTriggerImplementation(_timeBeforeFinishTrigger);

        private class TimeFinishTriggerImplementation : SpellTriggerImplementationBase
        {
            private readonly float _timeBeforeFinishTrigger;
            private bool _wasTriggered = false;

            public TimeFinishTriggerImplementation(float timeBeforeFinishTrigger) =>
                _timeBeforeFinishTrigger = timeBeforeFinishTrigger;

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other) =>
                SpellTriggerCheckStatusEnum.Ignore;

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
}