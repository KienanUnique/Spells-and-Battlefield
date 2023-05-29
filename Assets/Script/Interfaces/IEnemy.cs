using Enemies;

namespace Interfaces
{
    public interface IEnemy : ISpellInteractable, IEnemyCharacter, IIdHolder, IPhysicsInteractable, IMovable
    {
    }
}