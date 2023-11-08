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
        [SerializeField] private bool _ignoreCaster;

        public override ISpellTrigger GetImplementationObject()
        {
            return new CollisionEnterTriggerImplementation(_timeBeforeFinishTrigger, _ignoreCaster);
        }

        private class CollisionEnterTriggerImplementation : SpellTriggerImplementationBase
        {
            private readonly float _timeBeforeFinishTrigger;
            private readonly bool _ignoreCaster;

            public CollisionEnterTriggerImplementation(float timeBeforeFinishTrigger, bool ignoreCaster)
            {
                _timeBeforeFinishTrigger = timeBeforeFinishTrigger;
                _ignoreCaster = ignoreCaster;
            }

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
            {
                if (_ignoreCaster &&
                    other.TryGetComponent(out ISpellInteractable spellInteractable) &&
                    Caster == spellInteractable)
                {
                    return SpellTriggerCheckStatusEnum.Ignore;
                }

                return SpellTriggerCheckStatusEnum.Finish;
            }

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
            {
                return _timeBeforeFinishTrigger > timePassedFromInitialize
                    ? SpellTriggerCheckStatusEnum.Ignore
                    : SpellTriggerCheckStatusEnum.Finish;
            }
        }
    }
}