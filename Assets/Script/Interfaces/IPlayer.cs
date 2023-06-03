using Interfaces.Pickers;
using Spells;

namespace Interfaces
{
    public interface IPlayer : IPlayerInformationProvider, ISpellInteractable, ICaster, IInteractableCharacter, IIdHolder, IEnemyTarget,
        IPickableItemsPicker, IPhysicsInteractable, IPickableEffectPicker, IPickableSpellPicker, IMovable
    {
    }
}