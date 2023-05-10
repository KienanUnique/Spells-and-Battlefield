using Interfaces.Pickers;

namespace Interfaces
{
    public interface IPlayer : IPlayerInformation, ISpellInteractable, ICharacter, IInteractable, IEnemyTarget,
        IDroppedItemsPicker, IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
    }
}