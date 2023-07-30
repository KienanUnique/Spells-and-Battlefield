using Enemies;
using Enemies.Character;

namespace Interfaces
{
    public interface IEnemy : ISpellInteractable, IEnemyCharacter, IIdHolder, IPhysicsInteractable, IMovable, IToMovingPlatformStickable, ICaster
    {
    }
}