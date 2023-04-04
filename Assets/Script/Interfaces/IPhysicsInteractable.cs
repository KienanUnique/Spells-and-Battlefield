using UnityEngine;

namespace Interfaces
{
    public interface IPhysicsInteractable : IInteractable
    {
        public Vector3 CurrentPosition { get; }
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force);
    }
}