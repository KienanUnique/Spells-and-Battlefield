using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Factory;
using Zenject;

namespace Systems.Installers.Concrete_Types.Comics
{
    public class ComicsFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var factory = new ComicsScreenFactory(Container);
            Container.Bind<IComicsScreenFactory>().FromInstance(factory).AsSingle();
        }
    }
}