using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Interfaces;

namespace Enemies.Concrete_Types.Knight.Character
{
    public interface IKnightCharacter : ICharacterBase
    {
        void TryDamageTargetsWithSwordAttack(List<IEnemyTarget> targets);
    }
}