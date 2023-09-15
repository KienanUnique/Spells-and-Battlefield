using Interfaces.Pickers;
using Player;

namespace Interfaces
{
    public interface IPlayer : IPlayerInformationProvider,
        ISpellInteractable,
        ICaster,
        ISummoner,
        IInteractableCharacter,
        IIdHolder,
        IEnemyTarget,
        IPickableItemsPicker,
        IPhysicsInteractable,
        IPickableEffectPicker,
        IPickableSpellPicker,
        IMovable,
        IPlayerInitializationStatus,
        IToMovingPlatformStickable
    {
    }
}