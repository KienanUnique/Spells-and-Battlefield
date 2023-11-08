using Enemies.Look_Point_Calculator;

namespace Spells.Implementations_Interfaces.Implementations
{
    public interface ISpellMovementWithLookPointCalculator : ISpellMovement
    {
        public ILookPointCalculator GetLookPointCalculator();
    }
}