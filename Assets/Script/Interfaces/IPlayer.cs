using Spells;

namespace Interfaces
{
    public interface IPlayer : ISpellInteractable, ICharacter, IInteractable, IEnemyTarget, IDroppedItemsPicker,
        IPhysicsInteractable
    {
    }
}