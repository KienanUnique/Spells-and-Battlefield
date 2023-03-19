using UnityEngine;

namespace Interfaces
{
    public interface IPlayer : ISpellInteractable, ICharacter, IInteractable
    {
        public Transform MainTransform { get; }
    }
}