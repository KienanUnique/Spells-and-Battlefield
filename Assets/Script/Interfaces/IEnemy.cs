using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Character;

namespace Interfaces
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
        IInitializableWithActionsPool
    {
    }
}