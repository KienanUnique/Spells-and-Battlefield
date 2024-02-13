using Common.Abstract_Bases.Character;
using Common.Id_Holder;
using Common.Mechanic_Effects.Concrete_Types.Change_Speed;
using Common.Mechanic_Effects.Concrete_Types.Push;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies;
using Pickable_Items.Picker_Interfaces;
using Player.Press_Key_Interactor;
using Puzzles.Mechanisms.Moving_Platforms;
using Spells;

namespace Player
{
    public interface IPlayer : IPlayerInformationProvider,
        ISpellInteractable,
        ICaster,
        ISummoner,
        ICharacter,
        IIdHolder,
        IEnemyTarget,
        IPickableItemsPicker,
        IPhysicsInteractable,
        IPickableEffectPicker,
        IPickableSpellPicker,
        IMovable,
        IPlayerInitializationStatus,
        IToMovingPlatformStickable,
        IPlayerAsPressKeyInteractor
    {
    }
}