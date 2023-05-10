using UnityEngine;

namespace Interfaces
{
    public interface IPhysicsInformation : IInteractable
    {
        public Vector3 CurrentPosition { get; }
    }
}