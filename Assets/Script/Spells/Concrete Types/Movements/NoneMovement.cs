using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Spells.Abstract_Types.Scriptable_Objects.Parts;
using Spells.Data_For_Spell_Implementation;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Concrete_Types.Movements
{
    [CreateAssetMenu(fileName = "None Movement",
        menuName = ScriptableObjectsMenuDirectories.SpellMovementDirectory + "None Movement", order = 0)]
    public class NoneMovement : SpellMovementScriptableObject
    {
        public override ISpellMovementWithLookPointCalculator GetImplementationObject()
        {
            return new NoneMovementImplementation();
        }

        private class NoneMovementImplementation : ISpellMovementWithLookPointCalculator
        {
            public void Initialize(IDataForSpellImplementation data)
            {
            }

            public void StartMoving()
            {
            }

            public void StopMoving()
            {
            }

            public ILookPointCalculator GetLookPointCalculator()
            {
                return new KeepLookDirectionLookPointCalculator();
            }
        }
    }
}