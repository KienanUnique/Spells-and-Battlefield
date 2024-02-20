using System;
using System.Collections.Generic;
using Systems.Dialog;
using Systems.Dialog.Avatar_Storage;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Avatar.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    [CreateAssetMenu(fileName = "In Game Dialog Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "In Game Dialog Installer")]
    public class InGameDialogInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AvatarStorage _avatarStorage;
        [SerializeField] private AvatarViewSettings _viewSettings;

        public override void InstallBindings()
        {
            InstallDialogStarter();
            InstallDialogAvatarsStorage();
            InstallAvatarViewSettings();
        }

        private void InstallAvatarViewSettings()
        {
            Container.Bind<IAvatarViewSettings>().FromInstance(_viewSettings).AsSingle();
        }

        private void InstallDialogStarter()
        {
            var dialogStarter = new DialogService();

            Container.Bind(new List<Type> {typeof(IDialogStarterForGameManager), typeof(IDialogService)})
                     .FromInstance(dialogStarter)
                     .AsSingle();
        }

        private void InstallDialogAvatarsStorage()
        {
            Container.Bind<IAvatarStorage>().FromInstance(_avatarStorage).AsSingle();
        }
    }
}