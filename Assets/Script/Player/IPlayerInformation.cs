using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Transform;
using Player.Spell_Manager;

namespace Player
{
    public interface IPlayerInformationProvider : ICharacterInformationProvider,
        IPhysicsInformation,
        IPlayerSpellsManagerInformation,
        IPlayerDashInformation
    {
        public IReadonlyTransform CameraTransform { get; }
    }
}