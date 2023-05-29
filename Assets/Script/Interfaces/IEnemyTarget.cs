using UnityEngine;

namespace Interfaces
{
    public interface IEnemyTarget : IInteractableCharacter, IIdHolder
    {
        public Transform MainTransform { get; }
    }
}