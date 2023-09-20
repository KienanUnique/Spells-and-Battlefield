using UI.Element.View.Settings;
using UI.Loading_Window.View;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    [CreateAssetMenu(fileName = "UI Base Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "UI Base Settings Installer")]
    public sealed class UiBaseSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DefaultUIElementViewSettings _defaultUIElementViewSettings;
        [SerializeField] private LoadingWindowSettings _loadingWindowSettings;

        public override void InstallBindings()
        {
            BindUISettings();
        }

        private void BindUISettings()
        {
            Container.Bind<ILoadingWindowSettings>().FromInstance(_loadingWindowSettings).AsSingle();
            Container.Bind<IDefaultUIElementViewSettings>().FromInstance(_defaultUIElementViewSettings).AsSingle();
        }
    }
}