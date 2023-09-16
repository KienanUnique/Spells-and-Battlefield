using Common.Abstract_Bases.Character;
using Common.Mechanic_Effects.Concrete_Types.Summon;

namespace Enemies.Character
{
    public interface IEnemyCharacter : ICharacterBase, IEnemyTargetsEffectsApplier
    {
        public ISummoner Summoner { get; }
    }
}