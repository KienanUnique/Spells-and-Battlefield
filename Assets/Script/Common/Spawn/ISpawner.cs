using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Common.Spawn
{
    public interface ISpawner : IInitializable
    {
        public void Spawn();
    }
}