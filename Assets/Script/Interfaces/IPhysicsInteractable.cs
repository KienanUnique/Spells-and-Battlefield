using UnityEngine;

namespace Interfaces
{
    public interface IPhysicsInteractable : IPhysicsInformation
    {
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
    }
}