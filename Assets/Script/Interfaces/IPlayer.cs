using Interfaces.Pickers;
using Spells;

namespace Interfaces
{
    public interface IPlayer : IPlayerInformationProvider, ISpellInteractable, ICaster, IInteractableCharacter, IIdHolder, IEnemyTarget,
        IDroppedItemsPicker, IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
    }
}