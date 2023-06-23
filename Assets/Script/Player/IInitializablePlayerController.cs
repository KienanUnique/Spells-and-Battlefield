using Player.Setup;

namespace Player
{
    public interface IInitializablePlayerController
    {
        public void Initialize(IPlayerControllerSetupData setupData);
    }
}