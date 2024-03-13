using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Id_Holder;
using Common.Mechanic_Effects.Concrete_Types.Change_Fraction;
using Common.Mechanic_Effects.Concrete_Types.Change_Speed;
using Common.Mechanic_Effects.Concrete_Types.Push;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Character;
using Puzzles.Mechanisms.Moving_Platforms;
using Spells;

namespace Enemies
{
    public interface IEnemy : ISpellInteractable,
        IEnemyCharacter,
        IIdHolder,
        IPhysicsInteractable,
        IMovable,
        IToMovingPlatformStickable,
        ICaster,
        ISummoner,
        IEnemyTarget,
        IInitializableWithActionsPool,
        IFactionChangeable
    {
    }
}