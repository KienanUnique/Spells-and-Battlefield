using Common;
using Common.Readonly_Transform;
using Enemies.Look_Point_Calculator;

namespace Enemies.Look
{
    public interface IEnemyLook : IEnemyLookForStateMachine
    {
        public void StartLooking();
        public void SetLookPointCalculator(ILookPointCalculator lookPointCalculator);
        public void StopLooking();
    }

    public interface IEnemyLookForStateMachine : IReadonlyLook
    {
        public IReadonlyTransform ThisPositionReferencePointForLook { get; }
        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform);
    }
}