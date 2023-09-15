using Common;
using Common.Capsule_Size_Information;

namespace Enemies
{
    public interface IEnemyPrefabProvider : IPrefabProvider
    {
        public ICapsuleSizeInformation SizeInformation { get; }
    }
}