using Common.Abstract_Bases.Disableable;

namespace Enemies.Movement
{
    public interface IDisableableEnemyMovement : IEnemyMovement, IDisableable
    {
    }
}