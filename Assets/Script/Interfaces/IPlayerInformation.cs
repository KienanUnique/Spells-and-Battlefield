using Common.Readonly_Transform;
using Player.Spell_Manager;

namespace Interfaces
{
    public interface IPlayerInformationProvider : ICharacterInformationProvider,
        IPhysicsInformation,
        IPlayerSpellsManagerInformation,
        IPlayerDashInformation
    {
        public IReadonlyTransform CameraTransform { get; }
    }
}