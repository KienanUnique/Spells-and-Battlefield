using Interfaces.Pickers;

namespace Interfaces
{
    public interface IPlayer : ISpellInteractable, ICharacter, IInteractable, IEnemyTarget, IDroppedItemsPicker,
        IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
    }
}