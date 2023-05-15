using Interfaces.Pickers;
using Spells;

namespace Interfaces
{
    public interface IPlayer : IPlayerInformation, ISpellInteractable, ICaster, ICharacter, IIdHolder, IEnemyTarget,
        IDroppedItemsPicker, IPhysicsInteractable, IDroppedEffectPicker, IDroppedSpellPicker, IMovable
    {
    }
}