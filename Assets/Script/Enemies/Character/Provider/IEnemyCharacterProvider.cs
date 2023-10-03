﻿using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Readonly_Transform;

namespace Enemies.Character.Provider
{
    public interface IEnemyCharacterProvider
    {
        public IDisableableEnemyCharacter GetImplementationObject(ICoroutineStarter coroutineStarter,
            IReadonlyTransform thisTransform, ISummoner summoner = null);
    }
}