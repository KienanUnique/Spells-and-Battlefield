using Common.Readonly_Transform;
using UnityEngine;

namespace Common.Abstract_Bases.Movement
{
    public interface IPositionInformation
    {
        public Vector3 CurrentPosition { get; }
        public IReadonlyTransform MainTransform { get; }
    }
}