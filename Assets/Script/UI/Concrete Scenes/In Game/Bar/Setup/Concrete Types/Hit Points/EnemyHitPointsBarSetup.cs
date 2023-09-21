using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Controller;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Bar.Setup.Concrete_Types.Hit_Points
{
    public class EnemyHitPointsBarSetup : HitPointsBarSetupBase
    {
        [SerializeField] private EnemyController _enemyController;
        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => new[] {_enemyController};
        protected override ICharacterInformationProvider CharacterInformation => _enemyController;
    }
}