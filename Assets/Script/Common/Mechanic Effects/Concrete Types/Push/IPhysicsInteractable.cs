using Common.Abstract_Bases.Movement;
using UnityEngine;

namespace Common.Mechanic_Effects.Concrete_Types.Push
{
    public interface IPhysicsInteractable : IPositionInformation
    {
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
    }
}