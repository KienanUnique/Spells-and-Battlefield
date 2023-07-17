using Enemies.Look_Point_Calculator;

namespace Enemies.Look
{
    public interface IEnemyLook
    {
        public void StartLooking();
        public void SetLookPointCalculator(ILookPointCalculator lookPointCalculator);
        public void StopLooking();
    }
}