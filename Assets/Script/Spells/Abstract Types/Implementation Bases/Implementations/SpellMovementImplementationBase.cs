using Enemies.Look_Point_Calculator;
using Spells.Implementations_Interfaces.Implementations;

namespace Spells.Abstract_Types.Implementation_Bases.Implementations
{
    public abstract class SpellMovementImplementationBase : SpellImplementationBase,
        ISpellMovementWithLookPointCalculator
    {
        public abstract void StartMoving();

        public abstract void StopMoving();

        public abstract ILookPointCalculator GetLookPointCalculator();
    }
}