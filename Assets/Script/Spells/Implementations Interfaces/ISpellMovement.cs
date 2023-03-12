using UnityEngine;

public interface ISpellMovement
{
#nullable enable
    public abstract void Move(Rigidbody spellRigidbody, Transform? fromCastObjectTransform, float timePassedFromInitialize);
#nullable disable
}