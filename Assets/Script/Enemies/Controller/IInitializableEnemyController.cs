using Enemies.Setup.Controller_Setup_Data;

namespace Enemies.Controller
{
    public interface IInitializableEnemyController
    {
        void Initialize(IEnemyControllerSetupData setupData);
    }
}