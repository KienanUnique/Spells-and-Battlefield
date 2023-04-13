using UnityEngine;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractable
    {
        public Transform MainTransform { get; }
    }
}