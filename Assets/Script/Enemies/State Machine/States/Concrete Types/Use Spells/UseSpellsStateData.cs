﻿using Common.Interfaces;
using Enemies.Movement.Enemy_Data_For_Moving;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    [CreateAssetMenu(fileName = "Use Spells State Data",
        menuName = ScriptableObjectsMenuDirectories.StatesDataDirectory + "Use Spells State Data", order = 0)]
    public class UseSpellsStateData : ScriptableObject
    {
        [SerializeField] private SpellsSelectorProvider _spellsSelectorProvider;
        [SerializeField] private EnemyDataForMoving _dataForMoving;

        public IEnemyDataForMoving DataForMoving => _dataForMoving;

        public IEnemySpellSelector GetSpellSelector(ICoroutineStarter coroutineStarter)
        {
            return _spellsSelectorProvider.GetImplementationObject(coroutineStarter);
        }
    }
}