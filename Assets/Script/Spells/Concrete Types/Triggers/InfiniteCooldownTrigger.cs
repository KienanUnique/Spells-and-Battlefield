using Spells.Abstract_Types.Implementation_Bases.Implementations;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Triggers
{
    [CreateAssetMenu(fileName = "Infinite Cooldown Trigger",
        menuName = ScriptableObjectsMenuDirectories.SpellTriggerDirectory + "Infinite Cooldown Trigger", order = 0)]
    public class InfiniteCooldownTrigger : SpellTriggerScriptableObject
    {
        [SerializeField] private float _cooldownInSeconds;

        public override ISpellTrigger GetImplementationObject()
        {
            return new InfiniteCooldownTriggerImplementation(_cooldownInSeconds);
        }

        private class InfiniteCooldownTriggerImplementation : SpellTriggerImplementationBase
        {
            private readonly float _cooldownInSeconds;
            private float _lastTriggerTime;

            public InfiniteCooldownTriggerImplementation(float cooldownInSeconds)
            {
                _cooldownInSeconds = cooldownInSeconds;
            }

            public override SpellTriggerCheckStatusEnum CheckContact(Collider other)
            {
                return SpellTriggerCheckStatusEnum.Ignore;
            }

            public override SpellTriggerCheckStatusEnum CheckTime(float timePassedFromInitialize)
            {
                if (timePassedFromInitialize - _lastTriggerTime >= _cooldownInSeconds)
                {
                    _lastTriggerTime = timePassedFromInitialize;
                    return SpellTriggerCheckStatusEnum.HandleEffect;
                }

                return SpellTriggerCheckStatusEnum.Ignore;
            }
        }
    }
}