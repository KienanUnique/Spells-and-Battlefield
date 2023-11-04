using Common.Readonly_Rigidbody;

namespace Common.Abstract_Bases.Movement
{
    public interface IPhysicsInformation
    {
        public IReadonlyRigidbody MainRigidbody { get; }
    }
}