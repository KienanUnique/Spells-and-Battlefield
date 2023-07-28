using Enemies.Setup;

namespace Enemies.Controller
{
    public interface IInitializableEnemyController
    {
        void Initialize(IEnemyBaseSetupData setupData);
    }
}