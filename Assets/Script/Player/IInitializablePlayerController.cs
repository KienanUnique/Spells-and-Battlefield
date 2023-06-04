using Player.Setup;

namespace Player
{
    public interface IInitializablePlayerController
    {
        void Initialize(IPlayerControllerSetupData setupData);
    }
}