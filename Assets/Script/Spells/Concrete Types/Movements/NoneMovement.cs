using Interfaces;
using Spells.Abstract_Types.Scriptable_Objects;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Implementations_Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "None Movement",
        menuName = "Spells and Battlefield/Spell System/Movement/None Movement", order = 0)]
    public class NoneMovement : SpellMovementScriptableObject
    {
        public override ISpellMovement GetImplementationObject() => new NoneMovementImplementation();

        private class NoneMovementImplementation : ISpellMovement
        {
            public void Initialize(Rigidbody spellRigidbody, ICaster caster)
            {
            }

            public void UpdatePosition()
            {
            }
        }
    }
}