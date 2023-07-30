using Enemies.Look_Point_Calculator;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellMovement : ISpellImplementation
    {
        public void UpdatePosition();
    }

    public interface ISpellMovementWithLookPointCalculator : ISpellMovement
    {
        public ILookPointCalculator GetLookPointCalculator();
    }
}