using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Interfaces;

namespace Enemies.Concrete_Types.Knight.Character
{
    public interface IKnightCharacter : IEnemyCharacter
    {
        void TryDamageTargetsWithSwordAttack(List<IEnemyTarget> targets);
    }
}