using Interfaces;
using UnityEngine;

namespace Spells.Implementations_Interfaces
{
    public interface ISpellImplementation
    {
#nullable enable
        public void Initialize(Rigidbody spellRigidbody, Transform? casterTransform,
            ISpellInteractable casterCharacter);
#nullable disable
    }
}