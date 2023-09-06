using Systems.In_Game_Systems.Level_Finish_Zone;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Level_Finish_Zone
{
    public class LevelFinishZoneInstaller : MonoInstaller
    {
        [SerializeField] private LevelFinishZoneHolder _finishZoneHolder;

        public override void InstallBindings()
        {
            InstallFinishZone();
        }

        private void InstallFinishZone()
        {
            Container.Bind<ILevelFinishZone>().FromInstance(_finishZoneHolder.ObjectToHold).AsSingle();
        }
    }
}