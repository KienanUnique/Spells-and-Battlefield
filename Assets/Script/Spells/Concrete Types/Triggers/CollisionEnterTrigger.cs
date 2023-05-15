using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Collision Enter Trigger",
        menuName = ScriptableObjectsMenuDirectories.SpellTriggerDirectory + "Collision Enter Trigger", order = 0)]
    public class CollisionEnterTrigger : SpellTriggerScriptableObject
    {
        [SerializeField] private float _timeBeforeFinishTrigger;

        public override ISpellTrigger GetImplementationObject() =>
            new CollisionEnterTriggerImplementation(_timeBeforeFinishTrigger);

        private class CollisionEnterTriggerImplementation : SpellTriggerImplementationBase
        {
            private readonly float _timeBeforeFinishTrigger;

            public CollisionEnterTriggerImplementation(float timeBeforeFinishTrigger) =>
                _timeBeforeFinishTrigger = timeBeforeFinishTrigger;

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other) =>
                SpellTriggerCheckStatusEnum.Finish;

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize) =>
                _timeBeforeFinishTrigger > timePassedFromInitialize
                    ? SpellTriggerCheckStatusEnum.Ignore
                    : SpellTriggerCheckStatusEnum.Finish;
        }
    }
}