using UnityEngine;

namespace Common.Abstract_Bases.Movement
{
    public interface IPhysicsInformation
    {
        public Vector3 CurrentPosition { get; }
    }
}