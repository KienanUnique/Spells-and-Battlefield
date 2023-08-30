using Systems.In_Game_Systems.Level_Finish_Zone;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class LevelFinishZoneInstaller : MonoInstaller
    {
        [SerializeField] private LevelFinishZoneController _finishZone;

        public override void InstallBindings()
        {
            InstallFinishZone();
        }

        private void InstallFinishZone()
        {
            Container
                .Bind<ILevelFinishZone>()
                .FromInstance(_finishZone)
                .AsSingle();
        }
    }
}