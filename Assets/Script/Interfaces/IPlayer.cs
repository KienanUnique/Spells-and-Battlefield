using UnityEngine;

public interface IPlayer : ISpellInteractable, ICharacter, IInteractable
{
    public Transform MainTransform { get; }
}
