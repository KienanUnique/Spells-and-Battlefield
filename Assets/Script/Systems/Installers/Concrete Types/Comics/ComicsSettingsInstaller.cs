using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Comics
{
    [CreateAssetMenu(fileName = "Comics Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "Comics Settings Installer")]
    public class ComicsSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ComicsPanelSettings _panelSettings;

        public override void InstallBindings()
        {
            Container.Bind<IComicsPanelSettings>().FromInstance(_panelSettings).AsSingle();
        }
    }
}